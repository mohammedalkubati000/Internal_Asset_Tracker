
using Internal_Asset_Tracker.Data;
using Internal_Asset_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

public class AssetsController : Controller
{
    // 
    private readonly AppDbContext _db;
    
    public AssetsController(AppDbContext db) 
    {
        _db = db;
    }


    public ActionResult Index()
    {
        //Entity Framework Approach
        IEnumerable<Asset> depts = _db.Assets.ToList();
        return View(depts);
    }


    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Asset asset)
    {
        if (ModelState.IsValid)
        {
            _db.Assets.Add(asset);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", "Please fill all the required fields.");
        return View(asset);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        var asset = _db.Assets.Find(id);
        if (asset == null)
        {
            return NotFound();
        }
        return View(asset);
    }



    [HttpPost]
    public ActionResult Edit(Asset asset)
    {
        if (ModelState.IsValid)
        {
            _db.Assets.Update(asset);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", "Please fill all the required fields.");
        return View(asset);
    }


    [HttpGet]
    public ActionResult Delete(int id)
    {
        var asset = _db.Assets.Find(id);
        if (asset == null)
        {
            return NotFound();
        }
        return View(asset);
    }

    [HttpPost]
    [ActionName("Delete")]
    public ActionResult DeleteConfirmed (int id)
    {
        var asset = _db.Assets.Find(id);
        if (asset == null)
        {
            return NotFound();
        }
        _db.Assets.Remove(asset);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }





}


