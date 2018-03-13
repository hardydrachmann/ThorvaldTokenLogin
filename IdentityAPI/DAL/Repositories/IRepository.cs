using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAPI.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(int id);

        Task<int> Create(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(int id);
    }
}
