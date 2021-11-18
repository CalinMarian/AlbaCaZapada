using AlbaCaZapada.Data;
using AlbaCaZapada.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;

namespace AlbaCaZapada.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<Student> objList = _db.Students;

        //    return View(objList);
        //}

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

        //GET AddStudent
        public IActionResult AddStudent()
        {
            var groups = _db.Groups.ToList();

            IEnumerable<SelectListItem> GetSelectListItems()
            {
                return _db.Groups.Select(c => new SelectListItem()
                {
                    Text = c.GroupName,
                    Value = c.Id.ToString(),
                });
            };
            ViewBag.listName = GetSelectListItems();
            return View();
        }

        //POST AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student obj)
        {
            var group = _db.Groups.Find(obj.GroupId);
            var newStudent = new Student()
            {
                Name = obj.Name,
                BirthDate = obj.BirthDate,
                FirstParent = obj.FirstParent,
                SecondParent = obj.SecondParent,
                Details = obj.Details,
                InSchool = obj.InSchool,
                GroupId = obj.GroupId,
                Group = _db.Groups.Find(obj.Id)
            };
            if (ModelState.IsValid)
            {
                _db.Students.Add(newStudent);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET EditStudent
        public IActionResult EditStudent(int Id)
        {
            var obj = _db.Students.Find(Id);
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
            var obj = _db.Students.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult InactiveStudents()
        {
            IEnumerable<Student> objList = _db.Students;
            var objListSorted = objList.Where(x => x.InSchool == false);
            return View(objListSorted);
        }

        [HttpGet]
        public async Task<IActionResult> InactiveStudents(string sortOrder, string search)
        {
            ViewData["StudentSearch"] = search;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            var students = from x in _db.Students select x;
            var sortedStudents = students.Where(x => x.InSchool == false);
            if (!string.IsNullOrEmpty(search))
            {
                sortedStudents = sortedStudents.Where(x => x.Name.Contains(search));
            }
            sortedStudents = sortedStudents.OrderBy(s => s.Name);
            return View(await sortedStudents.AsNoTracking().ToListAsync());
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

