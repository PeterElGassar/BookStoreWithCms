using E_BookStore.DataAccess.Data;
using E_BookStore.DataAccess.Repository.IRepository;
using E_BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_BookStore.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            var companyIdDb = _db.Companies.SingleOrDefault(c => c.Id == company.Id);
            if (companyIdDb != null)
            {
                companyIdDb.Name = company.Name.Trim();
                companyIdDb.Slug = company.Name.Trim().Replace(" ", "-").Replace(".", "-").ToLower();

                companyIdDb.State = company.State;
                companyIdDb.StreetAddress = company.StreetAddress;
                companyIdDb.PostalCode = company.PostalCode;
                companyIdDb.City = company.City;
                companyIdDb.PhoneNumber = company.PhoneNumber;
                companyIdDb.IsVerifiedCompany = company.IsVerifiedCompany;
                _db.SaveChanges();
            }
        }
    }
}
