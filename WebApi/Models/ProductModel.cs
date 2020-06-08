using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebApi.Models
{
    /// <summary>
    /// hold product model information
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// unique key id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// product name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// product description
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// product price
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// product delivery price
        /// </summary>
        [Required]
        public decimal DeliveryPrice { get; set; }

        /// <summary>
        /// base constructor with default values
        /// </summary>
        public ProductModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// IDataReader constructor
        /// </summary>
        /// <param name="reader">reader</param>
        public ProductModel(IDataReader reader)
        {
            Id = Guid.Parse(reader["Id"].ToString());
            Name = reader["Name"].ToString();
            Description = (DBNull.Value == reader["Description"]) ? null : reader["Description"].ToString();
            Price = Convert.ToDecimal(reader["Price"]);
            DeliveryPrice = Convert.ToDecimal(reader["DeliveryPrice"]);
        }
    }
}