using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using E_BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
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
            var coverType = new CoverType();

        
            if (id == null)
                return View(coverType);

            var param = new DynamicParameters();
            param.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, param);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Upsert(CoverType modal)
        {

            string message = "";
            bool status;

            if (!ModelState.IsValid)
                return Json(new { status = false, message = "Form Invalid .." });

            //Need Stored Proc
            if (_unitOfWork.CoverType.GetAll(c => c.Id != modal.Id)
                .Any(c => c.Name.ToLower() == modal.Name.ToLower()))
                return Json(new { status = false, message = "CoverType Name Already Exist..", isExist = true });

            var param = new DynamicParameters();

            param.Add("@Name", modal.Name);

            if (modal.Id > 0)
            {
                //Edit
                param.Add("@Id", modal.Id);
                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, param);

                status = true;
                message = "CoverType Updated Success..";
            }
            else
            {
                //Create
                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, param);
                status = true;
                message = "CoverType Created Success..";
            }
            _unitOfWork.Complete();

            return Json(new { status = status, message = message });
        }


        #region Api Calls
        public IActionResult GetAllCategories()
        {
            var categories = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);

            return Json(new { data = categories });
        }

        public IActionResult DeleteCoverType(int id)
        {
            var param = new DynamicParameters();

            param.Add("@Id", id);
            //this Query Rather than Find() or SingleOrDefault()
            var coverTypeInDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, param);

            if (coverTypeInDb == null)
            {
                return Json(new { status = false, message = "Erorr While Deleting" });
            }

            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, param);
            _unitOfWork.Complete();

            return Json(new { status = true, message = "Delete Success.." });
        }


        #endregion
    }
}
