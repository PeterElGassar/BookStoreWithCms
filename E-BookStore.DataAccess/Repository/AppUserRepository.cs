using E_BookStore.DataAccess.Data;
using E_BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_BookStore.DataAccess.Repository.IRepository
{
    class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly ApplicationDbContext _db;
        public AppUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AppUser appUser)
        {
            var user = _db.AppUsers.SingleOrDefault(u => u.Id == appUser.Id);

            if (user != null)
            {
                user.Name = appUser.Name.Trim();
                user.Slug = appUser.Name.Trim().Replace(" ", "-").Replace(".", "-");
                user.StreetAddress = appUser.StreetAddress;
                user.City = appUser.City;
                user.State = appUser.State;
                user.PostalCode = appUser.PostalCode;
                user.Role = appUser.Role;
                user.CompanyId = appUser.CompanyId;

                _db.SaveChanges();
            }
        }
    }
}
