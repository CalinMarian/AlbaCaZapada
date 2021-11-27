using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AlbaCaZapada.View_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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
            ViewData["Ballance"] = student.Payments.Sum(x => x.Amount) - (student.Payments.Sum(x => x.DaysInSchool) * 10);
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
                    Amount = obj.Amount,
                    PaymentDate = obj.PaymentDate,
                    StudentId = obj.Id,
                    DaysInSchool = obj.DaysInSchool,
                    WorkingDaysInMonth = obj.WorkingDaysInMonth,
                    Month = obj.Month
                };
                _db.Payments.Add(newPayment);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = obj.Id });
            }
            return View(obj);
        }

        //GET EditPayment
        public IActionResult EditPayment(int Id)
        {
            var obj = _db.Payments.Find(Id);
            ViewData["StudentId"] = obj.StudentId;
            TempData["StudentId"] = obj.StudentId;
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
                _db.Payments.Update(obj).Property(x => x.StudentId).IsModified = false;
                _db.SaveChanges();
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
                var students = _db.Students.ToList();
                foreach (var student in students)
                {
                    Payment newPayment = new()
                    {
                        Month = obj.Month,
                        WorkingDaysInMonth = obj.WorkingDaysInMonth,
                        DaysInSchool = obj.WorkingDaysInMonth,
                        PaymentDate = System.DateTime.Today,
                        StudentId = student.Id
                    };
                    _db.Payments.Add(newPayment);
                }
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        //Get GroupDropdownForIndeptedStudents
        public IActionResult IndeptedStudentsByGroup()
        {
            var model = new StudentPaymentViewModel()
            {
                Groups = _db.Groups.Select(c => new SelectListItem()
                {
                    Text = c.GroupName,
                    Value = c.Id.ToString(),
                }),
                Students = Enumerable.Empty<Student>(),
                Payments = Enumerable.Empty<Payment>()
            };
            return View(model);
        }

        //GetIndeptedStudents
        public IActionResult IndeptedStudents(string group)
        {
            var model = new StudentPaymentViewModel()
            {
                Students = _db.Students.Where(m => m.GroupId == int.Parse("group"))
            };
            return PartialView("_IndeptedtudentsList", model);
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
