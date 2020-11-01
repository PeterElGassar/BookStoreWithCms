using E_BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace E_BookStore.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Find(int id);

        T FindBySulg(string sulg);

        //2 Condition For Filter Entity And Can Leave it Null
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null, string includeProperties = null

            );

        //3 Conditions For Filter Collection And Can Leave it Null
        IEnumerable<T> GetAll(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null

           );


        void Add(T entity);

        void Remove(int id);

        void RemoveBySulg(string sulg);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

    }
}
