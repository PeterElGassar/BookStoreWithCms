using E_BookStore.DataAccess.Data;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_BookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }


        //Here I Put Update Because It is Different  From Repo to another
        public void Update(Category category)
        {
            var entityInDb = _db.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (entityInDb != null)
            {
                entityInDb.Name = category.Name;
                entityInDb.Slug = category.Name.Trim().Replace(" ", "-").Replace(".", "-").ToLower();
                _db.SaveChanges();
            }
        }


        public override void Add(Category category)
        {
            category.Slug = category.Name.Trim()
                .Replace(" ", "-")
                .Replace(".", "-")
                .ToLower();    
            dbSet.Add(category);
        }

    }
}
