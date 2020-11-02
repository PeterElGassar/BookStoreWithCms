using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_BookStore.DataAccess.Data;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using E_BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        #region Api Calls
        public IActionResult GetAllUsers()
        {
            var userList = _context.AppUsers.Include(u => u.Company).ToList();
            //Mapping Role For every user
            var roles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();

            foreach (var user in userList)
            {
                string roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.SingleOrDefault(r => r.Id == roleId).Name;

                //
                if (user.Company == null)
                    user.Company = new Company { Name = "" };

            }
            return Json(new { data = userList });
        }

        public IActionResult DeleteBook(int id)
        {
            //var bookInDb = _unitOfWork.Book.Find(id);

            //if (bookInDb == null)
            //{
            //    return Json(new { status = false, message = "Erorr While Deleting" });
            //}
            //_unitOfWork.Book.DeleteImage(bookInDb.ImageUrl);
            //_unitOfWork.Book.Remove(bookInDb);
            //_unitOfWork.Complete();

            return Json(new { status = true, message = "Delete Success.." });
        }

        public JsonResult LockoutUser(string id)
        {
            var userInDb = _context.AppUsers.SingleOrDefault(u => u.Id == id);

            if (userInDb == null)
                return Json(new { status = false, message = "Something Wrong While Locking.." });

            if (userInDb.LockoutEnd < DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            else
            {
                return Json(new { status = false, message = "User Already Locking.." });
            }
            _context.SaveChanges();
            return Json(new { status = true, message = "User Locking Successfull.." });
        }

        public JsonResult UnLockoutUser([FromBody] string id)
        {
            var userInDb = _context.AppUsers.SingleOrDefault(u => u.Id == id);

            if (userInDb == null)
                return Json(new { status = false, message = "Something Wrong While UnLocking.." });

            if (userInDb.LockoutEnd > DateTime.Now)
            {
                //User is currently locked,we will Unlocking him
                userInDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                return Json(new { status = false, message = "User Already UnLocking.." });
            }
            _context.SaveChanges();
            return Json(new { status = true, message = "User UnLocking Successfull.." });
        }

        #endregion
    }
}
