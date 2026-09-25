using Internal_Asset_Tracker.Data;
using Internal_Asset_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Internal_Asset_Tracker.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _db;

        public DepartmentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<Department> depts = _db.Departments.ToList();
            return View(depts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Add(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(department);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var department = _db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Update(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(department);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var department = _db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var department = _db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            _db.Departments.Remove(department);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }








    }
}
