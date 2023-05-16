using Bulky.Models;
using Bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _CategoryRepo= db;
        }
        public IActionResult Index()
        {
            List<Category> CategoryList = _CategoryRepo.GetAll().ToList();
            return View(CategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exatly match the Name");
            }
            if (ModelState.IsValid)
            {
                _CategoryRepo.Add(obj);
				_CategoryRepo.Save();
                TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
            return View();
        }

		public IActionResult Edit(int? id)
		{
             if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDB = _CategoryRepo.Get(u=>u.id==id);
            if (categoryFromDB == null)
            {
				return NotFound();
			}
			return View(categoryFromDB);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_CategoryRepo.Update(obj);
				_CategoryRepo.Save();
				TempData["success"] = "Category updated successfully";
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
            Category categoryFromDB = _CategoryRepo.Get(u => u.id == id);
			if (categoryFromDB == null)
            {
				return NotFound();
			}
			return View(categoryFromDB);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
            Category obj = _CategoryRepo.Get(u => u.id == id);

			if (obj == null)
            {
               return NotFound();
            }
			_CategoryRepo.Remove(obj);
			_CategoryRepo.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
