using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Core.Shared;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
            Task<T> GetById(int id);
            Task<List<T>> ListAll();
            Task<T> Add(T entity);
            Task Update(T entity);
            Task Delete(T entity);
    }
}
