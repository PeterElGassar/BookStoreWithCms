using E_BookStore.DataAccess.Data;
using E_BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            SP_Call = new SP_Call(_context);
            CoverType = new CoverTypeRepository(_context);
            Book = new BookRepository(_context);
            Company = new CompanyRepository(_context);
            AppUser = new AppUserRepository(_context);
        }


        //Note Here Name Should Be Exact Like Name in InterFace
        public ICategoryRepository Category { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public ICoverTypeRepositosy CoverType { get; private set; }
        public IBookRepository Book { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IAppUserRepository AppUser { get; private set; }

        //Save Change in Glopal layer for every Repo
        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
