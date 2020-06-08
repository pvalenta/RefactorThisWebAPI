using System;

namespace Contracts.Models
{
    public interface IProductOption
    {
        Guid Id { get; set; }
        Guid ProductId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
