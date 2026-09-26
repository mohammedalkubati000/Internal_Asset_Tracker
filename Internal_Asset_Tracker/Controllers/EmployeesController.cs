using Internal_Asset_Tracker.Data;
using Internal_Asset_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internal_Asset_Tracker.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;

        public EmployeesController(AppDbContext db) 
        {
            _db = db;      
        
        }

        public ActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<Employee> depts = _db.Employees.Include(e => e.Department).ToList();
            return View(depts);
        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employee);
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]

        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }   


    }
}
