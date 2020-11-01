using E_BookStore.DataAccess.Data;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_BookStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepositosy
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var entityInDb = _db.CoverTypes.SingleOrDefault(c => c.Id == coverType.Id);
            if (entityInDb != null)
            {
                entityInDb.Name = coverType.Name;
                entityInDb.Slug = coverType.Name.Trim().Replace(" ", "-").Replace(".", "-").ToLower();

                _db.SaveChanges();
            }
        }
    }
}
