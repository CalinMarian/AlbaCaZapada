using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            ViewData["StudentSearch"] = search;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            var students = from x in _db.Students.Include(x => x.Group) select x;
            var sortedStudents = students.Where(x => x.InSchool == true);
            if (!string.IsNullOrEmpty(search))
            {
                sortedStudents = sortedStudents.Where(x => x.Name.Contains(search));
            }
            sortedStudents = sortedStudents.OrderBy(s => s.Name);
            return View(await sortedStudents.AsNoTracking().ToListAsync());
        }

        public void SetStudentBalance(int studentID)
        {
            var payments = _db.Payments.Include("Student").Where(x => x.StudentId == studentID).ToList();
            var balance = payments.Sum(x => x.Amount) - payments.Sum(x => x.AmountOwed);
            var student = new Student()
            {
                Id = studentID,
                Balance = balance
            };


            _db.Students.Attach(student);
            _db.Entry(student).Property(X => X.Balance).IsModified = true;
            _db.SaveChanges();


            payments.Count();
        }

        //GET AddStudent
        public IActionResult AddStudent()
        {
            var groups = _db.Groups.Where(x => x.IsActive == true).ToList();
            ViewBag.listName = GetActiveGroupSelectListItems();
            return View();
        }

        //POST AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET EditStudent
        public IActionResult EditStudent(int Id)
        {
            var obj = _db.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == Id);
            ViewBag.listName = GetActiveGroupSelectListItems();

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST EditStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudentPost(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();
                if (obj.InSchool == true)
                {
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("InactiveStudents");
            }
            return View(obj);
        }

        public IActionResult DetailsStudent(int Id)
        {
            var obj = _db.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> InactiveStudents(string sortOrder, string search)
        {
            ViewData["StudentSearch"] = search;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            var students = from x in _db.Students.Include(x => x.Group) select x;
            var sortedStudents = students.Where(x => x.InSchool == false);
            if (!string.IsNullOrEmpty(search))
            {
                sortedStudents = sortedStudents.Where(x => x.Name.Contains(search));
            }
            sortedStudents = sortedStudents.OrderBy(s => s.Name);

            foreach (var stud in sortedStudents)
            {
                List<Payment> payments = _db.Payments.Where(x => x.StudentId == stud.Id).ToList();
            }

            return View(await sortedStudents.AsNoTracking().ToListAsync());
        }

        public IEnumerable<SelectListItem> GetActiveGroupSelectListItems()
        {
            return _db.Groups.Where(x => x.IsActive == true).Select(c => new SelectListItem()
            {
                Text = c.GroupName,
                Value = c.Id.ToString(),
            });
        }



        //GET DeleteStudent
        //public IActionResult DeleteStudent(int Id)
        //{
        //    var obj = _db.Students.Find(Id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}

        //POST DeleteStudent
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteStudentPost(Student obj)
        //{
        //    _db.Students.Remove(obj);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //GET DetailsStudent


    }
}

