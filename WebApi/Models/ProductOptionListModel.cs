using System.Collections.Generic;

namespace WebApi.Models
{
    /// <summary>
    /// hold item list model
    /// </summary>
    public class ProductOptionListModel
    {
        /// <summary>
        /// list of items
        /// </summary>
        public List<ProductOptionModel> Items { get; set; }
    }
}