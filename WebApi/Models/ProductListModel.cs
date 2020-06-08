using Contracts.Models;
using System.Collections.Generic;

namespace WebApi.Models
{
    /// <summary>
    /// hold items list model
    /// </summary>
    public class ProductListModel
    {
        /// <summary>
        /// list of items
        /// </summary>
        public List<IProduct> Items { get; set; }
    }
}