using System;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IBaseRepository<T> : IDisposable
    {
        Task<T> GetByIdAsync(Guid id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
