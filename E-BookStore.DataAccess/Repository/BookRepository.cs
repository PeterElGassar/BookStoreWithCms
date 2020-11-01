using E_BookStore.DataAccess.Data;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using E_BookStore.Models.ViewModels;

namespace E_BookStore.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }


        private readonly string webRootPath = Path
            .Combine(String.Format(@"{0}\wwwroot", Directory.GetCurrentDirectory()));

        //Here I Put Update Because It is Different  From Repo to another
        public void Update(Book book)
        {
            var entityInDb = _db.Books.FirstOrDefault(c => c.Id == book.Id);

            if (entityInDb != null)
            {
                entityInDb.Title = book.Title.Trim();
                entityInDb.Slug = book.Title.Trim().Replace(" ", "-").Replace(".", "-").ToLower();
                entityInDb.Author = book.Author;
                entityInDb.Description = book.Description;
                entityInDb.SIBN = book.SIBN;

                entityInDb.ImageUrl = book.ImageUrl;
                //Prices
                entityInDb.Price = book.Price;
                entityInDb.ListPrice = book.ListPrice;
                entityInDb.Price50 = book.Price50;
                entityInDb.Price100 = book.Price100;
                //Dropdowns
                entityInDb.CategoryId = book.CategoryId;
                entityInDb.CoverTypeId = book.CoverTypeId;

                _db.SaveChanges();
            }
        }

        public override void Add(Book book)
        {
            book.Slug = book.Title.Trim()
                .Replace(" ", "-")
                .Replace(".", "-")
                .ToLower();
            dbSet.Add(book);
        }


        public void UploadImage(IFormFileCollection files, BookVM modal)
        {
            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Uploads\BooksImages\");
                var extenstion = Path.GetExtension(files[0].FileName);

                //1-Delete old image
                if (modal.Book.ImageUrl != null)
                {
                    DeleteImage(modal.Book.ImageUrl);
                }
                //2-upload new img
                using (var filesStream = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                {
                    //copy file from form inside filesStream
                    files[0].CopyTo(filesStream);
                }
                modal.Book.ImageUrl = @"\Uploads\BooksImages\" + fileName + extenstion;
            }
        }

        public void DeleteImage(string ImageUrl)
        {
            if (ImageUrl != null)
            {
                var imagePath = Path.Combine(webRootPath, ImageUrl.TrimStart('\\'));

                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
           
        }
    }
}
