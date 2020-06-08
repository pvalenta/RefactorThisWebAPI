using System;
using System.Data;

namespace Contracts.Models
{
    public interface IProduct
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        decimal DeliveryPrice { get; set; }
    }
}
