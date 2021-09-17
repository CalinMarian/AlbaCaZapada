using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                    StudentId = obj.Id
                };
                _db.Payments.Add(newPayment);
                _db.SaveChanges();
                return RedirectToAction("Index",new { id = obj.Id});
            }
            return View(obj);
        }

        //GET DeletePayment
        public IActionResult DeletePayment(int id)
        {
            var obj = _db.Payments.Find(id);
            ViewData["StudentId"] = TempData["StudentId"] = obj.StudentId;
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST DeletePayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePaymentPost(Payment obj)
        {
            _db.Payments.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = TempData["StudentId"] });
        }

    }
}
