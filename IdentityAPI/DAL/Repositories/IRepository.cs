using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);
    }
}
