using Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IProductRepository : IBaseRepository<IProduct>
    {
        Task<List<IProduct>> GetAllAsync();
        Task<List<IProduct>> GetByNameAsync(string name);
        Task CreateAsync(IProduct entity);
    }
}
