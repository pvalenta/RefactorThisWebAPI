using Contracts.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Entities.Models
{
    /// <summary>
    /// hold product option information
    /// </summary>
    public class ProductOptionModel : IProductOption
    {
        /// <summary>
        /// unique key id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// parent product id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// option name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// option description
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// base constructor
        /// </summary>
        public ProductOptionModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// IDataReader constructor
        /// </summary>
        /// <param name="reader">reader</param>
        public ProductOptionModel(IDataReader reader)
        {
            Id = Guid.Parse(reader["Id"].ToString());
            ProductId = Guid.Parse(reader["ProductId"].ToString());
            Name = reader["Name"].ToString();
            Description = (DBNull.Value == reader["Description"]) ? null : reader["Description"].ToString();
        }
    }
}