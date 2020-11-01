using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_BookStore.Models.Models;
using E_BookStore.DataAccess.Data;

namespace E_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoard : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashBoard(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/DashBoard
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

    }
}
