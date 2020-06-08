using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IProductOptionRepository : IBaseRepository<IProductOption>
    {
        Task<List<IProductOption>> GetByProductIdAsync(Guid productId);
        Task CreateAsync(Guid productId, IProductOption entity);
    }
}
