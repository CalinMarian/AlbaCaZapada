using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> objList = _db.Students;
            return View(objList);
        }

        //GET AddStudent
        public IActionResult AddStudent()
        {
            return View();
        }

        //POST AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student obj)
        {
            _db.Students.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
