using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace E_BookStore.Models.Models
{
    public class Book : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public string SIBN { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }

        //Prices
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        //Foreign Keys
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }


        public void UploadImage(IFormFileCollection files, string webRootPath)
        {

            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Uploads\BooksImages\");
                var extenstion = Path.GetExtension(files[0].FileName);

                //Delete old image
                if (this.ImageUrl != null)
                {
                    //...
                    DeleteOldImage(webRootPath);
                }
                //upload new img
                using (var filesStream = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                {
                    //copy file from form inside filesStream
                    files[0].CopyTo(filesStream);
                }
                this.ImageUrl = @"\Uploads\BooksImages\" + fileName + extenstion;
            }
            //else
            //{
            //    //if img not changed in update model
            //    if (this.Id != 0)
            //    {
            //        var bookInDb = this.Find(this.Id);
            //        this.ImageUrl = bookInDb.ImageUrl;
            //    }
            //}
        }

        private void DeleteOldImage(string webRootPath)
        {
            var imagePath = Path.Combine(webRootPath, this.ImageUrl.TrimStart('\\'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

    }
}
