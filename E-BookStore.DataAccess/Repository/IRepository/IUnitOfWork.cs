using E_BookStore.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        //this interface or Abstraction not dependent on Details 
        ICategoryRepository Category { get; }

        ICoverTypeRepositosy CoverType { get; }
        IBookRepository Book { get; }
        ICompanyRepository Company { get; }
        IAppUserRepository AppUser { get; }

        ISP_Call SP_Call { get; }



        void Complete();
    }
}
