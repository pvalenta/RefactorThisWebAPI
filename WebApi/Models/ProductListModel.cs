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
        public List<ProductModel> Items { get; set; }
    }
}