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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
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
            var category = new Company();

            if (id == null)
                return View(category);

            category = _unitOfWork.Company.Find(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Upsert(Company modal)
        {
            string message = "";
            bool status;

            if (!ModelState.IsValid)
                return Json(new { status = false, message = "Form Invalid .." });

            if (_unitOfWork.Company.GetAll(c => c.Id != modal.Id)
                .Any(c => c.Name.ToLower() == modal.Name.ToLower()))
                return Json(new { status = false, message = "Company Name Already Exist..", isExist = true });

            if (modal.Id > 0)
            {
                //Edit
                _unitOfWork.Company.Update(modal);
                status = true;
                message = "Company Updated Success..";
            }
            else
            {
                //Create
                _unitOfWork.Company.Add(modal);
                status = true;
                message = "Company Created Success..";
            }
            _unitOfWork.Complete();

            return Json(new { status = status, message = message });
        }


        #region Api Calls
        public IActionResult GetAllCompanies()
        {
            var categories = _unitOfWork.Company.GetAll();

            return Json(new { data = categories });
        }

        public IActionResult DeleteCompany(int id)
        {
            var categoryInDb = _unitOfWork.Company.Find(id);

            if (categoryInDb == null)
            {
                return Json(new { status = false, message = "Erorr While Deleting" });
            }
            _unitOfWork.Company.Remove(categoryInDb);
            _unitOfWork.Complete();

            return Json(new { status = true, message = "Delete Success.." });
        }


        #endregion
    }
}
