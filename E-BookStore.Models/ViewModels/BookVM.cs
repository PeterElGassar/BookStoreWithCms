using E_BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.Models.ViewModels
{
   public  class BookVM
    {
        public Book Book { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<SelectListItem> CoverTypes { get; set; }


    }
}
