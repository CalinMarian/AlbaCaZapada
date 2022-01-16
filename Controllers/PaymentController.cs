using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
using AlbaCaZapada.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Payments
        public IActionResult Index(int Id)
        {
            var student = _db.Students.Include("Payments").FirstOrDefault(x => x.Id == Id);
            var payment = student.Payments.OrderByDescending(x => x.PaymentDate);
            ViewData["StudentName"] = student.Name;
            ViewData["StudentId"] = student.Id;
            ViewData["StudentInSchool"] = student.InSchool.ToString();
            ViewData["Ballance"] = student.Payments.Sum(x => x.Amount) - student.Payments.Sum(x => x.AmountOwed);
            if (student == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        //GET AddPayment
        public IActionResult AddPayment(int Id)
        {
            var student = _db.Students.Include("Payments").FirstOrDefault(x => x.Id == Id);
            ViewData["StudentName"] = student.Name;
            ViewData["StudentId"] = student.Id;
            if (student == null)
            {
                return NotFound();
            }
            return View();
        }

        //Post AddPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPayment(Payment obj)
        {
            if (ModelState.IsValid)
            {
                Payment newPayment = new()
                {
                    Month = obj.Month,
                    WorkingDaysInMonth = obj.WorkingDaysInMonth,
                    DaysInSchool = obj.DaysInSchool,
                    DaysOutSchool = obj.WorkingDaysInMonth - obj.DaysInSchool,
                    PaymentDate = obj.PaymentDate,
                    StudentId = obj.Id,
                    Amount = obj.Amount,
                    AmountOwed = (obj.DaysInSchool * obj.Fee),
                    Fee = obj.Fee
                };
                _db.Payments.Add(newPayment);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Plata adaugata cu succes!";

                UpdateStudentBalanceValue(obj.Id);
                _db.SaveChanges();

                return RedirectToAction("Index", new { id = obj.Id });
            }
            return View(obj);
        }

        //GET EditPayment
        public IActionResult EditPayment(int Id)
        {
            var obj = _db.Payments.FirstOrDefault(x => x.Id == Id);
            ViewData["StudentId"] = TempData["StudentId"] = obj.StudentId;
            var student = _db.Students.Find(obj.StudentId);
            ViewData["StudentName"] = student.Name.ToString();
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST EditPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPaymentPost(Payment obj)
        {
            if (ModelState.IsValid)
            {

                Payment editedPayment = new()
                {
                    Id = obj.Id,
                    Month = obj.Month,
                    WorkingDaysInMonth = obj.WorkingDaysInMonth,
                    DaysInSchool = obj.DaysInSchool,
                    DaysOutSchool = obj.WorkingDaysInMonth - obj.DaysInSchool,
                    PaymentDate = obj.PaymentDate,
                    Amount = obj.Amount,
                    AmountOwed = (obj.DaysInSchool * obj.Fee),
                    Fee = obj.Fee,
                    StudentId = (int)TempData["StudentId"]
                };

                _db.Payments.Update(editedPayment);
                _db.SaveChanges();

                UpdateStudentBalanceValue((int)TempData["StudentId"]);

                _db.SaveChanges();
                TempData["AlertMessage"] = "Plata modificata cu succes!";
                return RedirectToAction("Index", new { id = TempData["StudentId"] });
            }
            return View(obj);
        }

        //GET AddNewMonthCharge
        public IActionResult AddNewMonthCharge()
        {
            return View();
        }

        //Post AddNewMonthCharge
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewMonthCharge(Payment obj)
        {
            if (ModelState.IsValid)
            {
                var students = _db.Students.Where(x => x.InSchool == true).ToList();
                foreach (var student in students)
                {
                    Payment newPayment = new()
                    {
                        Month = obj.Month,
                        WorkingDaysInMonth = obj.WorkingDaysInMonth,
                        DaysInSchool = obj.WorkingDaysInMonth,
                        DaysOutSchool = obj.WorkingDaysInMonth - obj.WorkingDaysInMonth,
                        PaymentDate = System.DateTime.Today,
                        StudentId = student.Id,
                        Amount = 0,
                        AmountOwed = obj.WorkingDaysInMonth * obj.Fee,
                        Fee = obj.Fee
                    };
                    _db.Payments.Add(newPayment);
                    _db.SaveChanges();
                    TempData["AlertMessage"] = "Taxa adaugata cu succes pt fiecare elev din gradinita!";
                    UpdateStudentBalanceValue(student.Id);
                }
                _db.SaveChanges();
                TempData["AlertMessage"] = "Taxa adaugata cu succes pt fiecare elev din gradinita!";
            }
            return RedirectToAction("Index", "Student");
        }

        public void UpdateStudentBalanceValue(int objId)
        {
            var allPayments = _db.Payments.Where(x => x.StudentId == objId);

            var student = _db.Students.Find(objId);

            student.Balance = allPayments.Sum(x => x.Amount) - allPayments.Sum(x => x.AmountOwed);

            _db.Students.Update(student);
        }

        //Get IndeptedStudents
        public IActionResult IndeptedStudents()
        {
            var groupWithStudents = _db.Groups.Where(x => x.IsActive).Include("Students");
            return View(groupWithStudents);
        }

        //Get ToatlPayments
        public IActionResult TotalPayments()
        {
            List<TotalPaymentPerMonth> totalPaymentPerMonth = new List<TotalPaymentPerMonth>();
            List<string> monthsList = new List<string>();
            var paymentsList = _db.Payments.ToList();
            foreach (var payment in paymentsList)      
            {
                if (!monthsList.Contains(payment.Month.ToString()))
                {
                    monthsList.Add(payment.Month.ToString());
                }
            }
            List<string> sumAmountPerMonth = new List<string>();
            foreach (var month in monthsList)
            {
                var allPaymenstOfMonth = paymentsList.FindAll(x => x.Month == month);
                var totalPayments = allPaymenstOfMonth.Sum(x => x.Amount);
                sumAmountPerMonth.Add(totalPayments.ToString());
            }

            for (int i = 0; i < monthsList.Count; i++)
            {
                totalPaymentPerMonth.Add(
                new TotalPaymentPerMonth()
                {
                    Month = monthsList[i],
                    TotalPayment = sumAmountPerMonth[i]
                });
            }
            return View(totalPaymentPerMonth);
        }

      
        //GET DeletePayment
        //public IActionResult DeletePayment(int id)
        //{
        //    var obj = _db.Payments.Find(id);
        //    ViewData["StudentId"] = TempData["StudentId"] = obj.StudentId;
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}

        //POST DeletePayment
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeletePaymentPost(Payment obj)
        //{
        //    _db.Payments.Remove(obj);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index", new { id = TempData["StudentId"] });
        //}

    }
}
