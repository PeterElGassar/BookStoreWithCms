using E_BookStore.Models.Models;
using E_BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.DataAccess.Repository.IRepository
{
    public interface IBookRepository :IRepository<Book>
    {
        void Update(Book book);

        void UploadImage(IFormFileCollection files, BookVM modal);

        void DeleteImage(string ImageUrl);
    }
}
