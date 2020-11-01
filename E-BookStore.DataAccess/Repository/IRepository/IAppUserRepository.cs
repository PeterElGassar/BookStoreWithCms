using E_BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.DataAccess.Repository.IRepository
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        void Update(AppUser appUser);
    }
}
