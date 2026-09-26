using Internal_Asset_Tracker.Data;
using Internal_Asset_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Internal_Asset_Tracker.Controllers
{
    public class AssetAssignmentsController : Controller
    {
        private readonly AppDbContext _db;
        
        public AssetAssignmentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<AssetAssignment> depts = _db.AssetAssignments.Include(a => a.Asset).Include(a => a.Employee).ToList();
            return View(depts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AssetId = new SelectList(_db.Assets, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(_db.Employees, "Id", "FullName");
            return View();
            
        }

        [HttpPost]
        public ActionResult Create(AssetAssignment assetassignment)
        {
            if (ModelState.IsValid)
            {
                _db.AssetAssignments.Add(assetassignment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(assetassignment);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var assetassignment = _db.AssetAssignments.Find(id);
            if (assetassignment == null)
            {
                return NotFound();
            }
            return View(assetassignment);
        }



        [HttpPost]
        public ActionResult Edit(AssetAssignment assetassignment)
        {
            if (ModelState.IsValid)
            {
                _db.AssetAssignments.Update(assetassignment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(assetassignment);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var assetassignment = _db.AssetAssignments.Find(id);
            if (assetassignment == null)
            {
                return NotFound();
            }
            return View(assetassignment);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var assetassignment = _db.Assets.Find(id);
            if (assetassignment == null)
            {
                return NotFound();
            }
            _db.Assets.Remove(assetassignment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
