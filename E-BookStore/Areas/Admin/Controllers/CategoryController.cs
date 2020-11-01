using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var category = new Category();

            if (id == null)
                return View(category);

            category = _unitOfWork.Category.Find(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Upsert(Category modal)
        {

            string message = "";
            bool status;

            if (!ModelState.IsValid)
                return Json(new { status = false, message = "Form Invalid .." });

            if (_unitOfWork.Category.GetAll(c => c.Id != modal.Id)
                .Any(c => c.Name.ToLower() == modal.Name.ToLower()))
                return Json(new { status = false, message = "Category Name Already Exist..", isExist = true });

            if (modal.Id > 0)
            {
                //Edit
                _unitOfWork.Category.Update(modal);
                status = true;
                message = "Category Updated Success..";
            }
            else
            {
                //Create
                _unitOfWork.Category.Add(modal);
                status = true;
                message = "Category Created Success..";
            }
            _unitOfWork.Complete();

            return Json(new { status = status, message = message });
        }


        #region Api Calls
        public IActionResult GetAllCategories()
        {
            var categories = _unitOfWork.Category.GetAll();

            return Json(new { data = categories });
        }

        public IActionResult DeleteCategory(int id)
        {
            var categoryInDb = _unitOfWork.Category.Find(id);

            if (categoryInDb == null)
            {
                return Json(new { status = false, message = "Erorr While Deleting" });
            }
            _unitOfWork.Category.Remove(categoryInDb);
            _unitOfWork.Complete();

            return Json(new { status = true, message = "Delete Success.." });
        }


        #endregion
    }
}
