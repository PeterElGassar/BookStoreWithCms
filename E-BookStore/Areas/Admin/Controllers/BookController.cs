using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using E_BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnviroment;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var bookVM = new BookVM()
            {
                Book = new Book(),
                Categories = _unitOfWork.Category.GetAll(),
                CoverTypes = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                })
            };

            if (id == null)
                return View(bookVM);

            bookVM.Book = _unitOfWork.Book.Find(id.GetValueOrDefault());

            if (bookVM.Book == null)
                return NotFound();

            return View(bookVM);
        }

        [HttpPost]
        public IActionResult Upsert(BookVM model)
        {
            //Fill lists
            model.Categories = _unitOfWork.Category.GetAll();
            model.CoverTypes = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem
            {
                Text = ct.Name,
                Value = ct.Id.ToString()
            });

            if (!ModelState.IsValid)
                return View(model);

            var files = HttpContext.Request.Form.Files;

            if (model.Book.Id > 0)
            {
                _unitOfWork.Book.UploadImage(files, model);

                _unitOfWork.Book.Update(model.Book);
                TempData["SM"] = "Book Updated Successfull..";
            }
            else
            {
                //create
                _unitOfWork.Book.UploadImage(files, model);
                _unitOfWork.Book.Add(model.Book);
                TempData["SM"] = "Book Added Success..";
            }
            _unitOfWork.Complete();

            return View(model);
        }

        #region Api Calls
        public IActionResult GetAllBooks()
        {
            var Books = _unitOfWork.Book.GetAll(includeProperties: "Category,CoverType");

            return Json(new { data = Books });
        }

        public IActionResult DeleteBook(int id)
        {
            var bookInDb = _unitOfWork.Book.Find(id);

            if (bookInDb == null)
            {
                return Json(new { status = false, message = "Erorr While Deleting" });
            }
            _unitOfWork.Book.DeleteImage(bookInDb.ImageUrl);
            _unitOfWork.Book.Remove(bookInDb);
            _unitOfWork.Complete();

            return Json(new { status = true, message = "Delete Success.." });
        }


        #endregion
    }
}
