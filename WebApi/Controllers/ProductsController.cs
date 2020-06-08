using System;
using System.Net;
using System.Web.Http;
using WebApi.Repositories;
using WebApi.Models;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// retrieve all products
        /// </summary>
        /// <returns>all products</returns>
        [Route]
        [HttpGet]
        public async Task<ProductListModel> GetAll()
        {
            var repository = new ProductRepository();
            var model = new ProductListModel { Items = await repository.GetAllProductsAsync() };

            return model;
        }

        /// <summary>
        /// retrieve products with name contain text
        /// </summary>
        /// <param name="name">product name</param>
        /// <returns>matching products</returns>
        [Route]
        [HttpGet]
        public async Task<ProductListModel> SearchByName(string name)
        {
            var repository = new ProductRepository();
            var model = new ProductListModel { Items = await repository.GetProductsByNameAsync(name) };

            return model;
        }

        /// <summary>
        /// retrieve product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>matching product</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<ProductModel> GetProduct(Guid id)
        {
            var repository = new ProductRepository();
            var model = await repository.GetProductAsync(id);

            if (model == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return model;
        }

        /// <summary>
        /// create product
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>operation result</returns>
        [Route]
        [HttpPost]
        public async Task<HttpResponseMessage> Create(ProductModel product)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var repository = new ProductRepository();
            await repository.CreateProductAsync(product);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="product">product</param>
        /// <returns>operation result</returns>
        [Route("{id}")]
        [HttpPut]
        public async Task<HttpResponseMessage> Update(Guid id, ProductModel product)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var repository = new ProductRepository();
            var current = await repository.GetProductAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            current.Name = product.Name;
            current.Description = product.Description;
            current.Price = product.Price;
            current.DeliveryPrice = product.DeliveryPrice;

            await repository.UpdateProductAsync(current);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>operation result</returns>
        [Route("{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            var repository = new ProductRepository();
            var current = await repository.GetProductAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            await repository.DeleteProductAsync(current.Id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// get product options
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns>list of product options</returns>
        [Route("{productId}/options")]
        [HttpGet]
        public async Task<ProductOptionListModel> GetOptions(Guid productId)
        {
            var repository = new ProductOptionRepository();
            var model = new ProductOptionListModel { Items = await repository.GetAllProductOptionsAsync(productId) };

            return model;
        }

        /// <summary>
        /// get product option
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="id">option id</param>
        /// <returns>product option</returns>
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public async Task<ProductOptionModel> GetOption(Guid productId, Guid id)
        {
            var repository = new ProductOptionRepository();
            var model = await repository.GetProductOptionAsync(id);

            if (model == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return model;
        }

        /// <summary>
        /// create product option
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="option">option</param>
        [Route("{productId}/options")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateOption(Guid productId, ProductOptionModel option)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var repository = new ProductOptionRepository();
            await repository.CreateProductOptionAsync(productId, option);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        /// <summary>
        /// update product option
        /// </summary>
        /// <param name="id">option id</param>
        /// <param name="option">option</param>
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateOption(Guid id, ProductOptionModel option)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var repository = new ProductOptionRepository();
            var current = await repository.GetProductOptionAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            current.Name = option.Name;
            current.Description = option.Description;

            await repository.UpdateProductOptionAsync(current);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        /// <summary>
        /// delete product option
        /// </summary>
        /// <param name="id">option id</param>
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteOption(Guid id)
        {
            var repository = new ProductOptionRepository();
            var current = await repository.GetProductOptionAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            await repository.DeleteProductOptionAsync(current.Id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
