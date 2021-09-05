using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
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
            var student = _db.Students.FirstOrDefault(x => x.Id == Id);
            ViewBag.Name = student.Name;
            ViewData ["payment"] = student.Payments.ToList();
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        //GET AddPayment
        public IActionResult AddPayment()
        {
            return View();
        }

        //Post AddPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPayment(Payment obj)
        {
            if (ModelState.IsValid)
            {
                _db.Payments.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
