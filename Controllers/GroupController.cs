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
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GroupController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Group> objList = _db.Groups;
            return View(objList);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            ViewData["GroupSearch"] = search;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            var group = from x in _db.Groups select x;
            var sortedGruoups = group;
            if (!string.IsNullOrEmpty(search))
            {
                sortedGruoups = sortedGruoups.Where(x => x.GroupName.Contains(search));
            }
            sortedGruoups = sortedGruoups.OrderBy(s => s.GroupName);
            return View(await sortedGruoups.AsNoTracking().ToListAsync());
        }

        //GET AddGroup
        public IActionResult AddGroup()
        {
            return View();
        }

        //POST AddGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGroup(Group obj)
        {
            if (ModelState.IsValid)
            {
                _db.Groups.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET EditStudent
        public IActionResult EditGroup(int Id)
        {
            var obj = _db.Groups.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST EditGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGroupPost(Group obj)
        {
            if (ModelState.IsValid)
            {
                _db.Groups.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);
        }
    }
}
