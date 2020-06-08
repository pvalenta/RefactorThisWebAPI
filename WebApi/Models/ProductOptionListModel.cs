using Contracts.Models;
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
        public List<IProductOption> Items { get; set; }
    }
}