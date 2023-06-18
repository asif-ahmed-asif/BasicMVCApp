using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }
        public IActionResult Index(string search)
        {
            if (search != null)
            {
                IEnumerable<Category> searchCategories = _db.Categories.Where(s => s.Name.StartsWith(search));
                return View(searchCategories);
            }
            //var categories = _db.Categories.ToList();

            //strongly typed
            IEnumerable<Category> categories = _db.Categories;
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order can not be same as Name");
            }
            var existedCategory = _db.Categories.Where(e => e.Name == category.Name);
            if (existedCategory.Count() > 0)
            {
                ModelState.AddModelError("Name", "This Name is already exists!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var category = _db.Categories.SingleOrDefault(c => c.Id == id);
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order can not be same as Name");
            }
            var existedCategory = _db.Categories.Where(e => e.Name == category.Name);
            if (existedCategory.Count() > 0)
            {
                ModelState.AddModelError("Name", "This Name is already exists!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
