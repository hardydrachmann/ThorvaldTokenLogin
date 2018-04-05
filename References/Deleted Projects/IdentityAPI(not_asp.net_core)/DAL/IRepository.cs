using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAPI.DAL.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(int id);

        T Create(T entity);

        T Update(T entity);

        bool Delete(T entity);
    }
}
