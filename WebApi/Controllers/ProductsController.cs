using System;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Contracts.Repositories;
using Contracts.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        IProductRepository productRepository;
        IProductOptionRepository productOptionRepository;

        /// <summary>
        /// constructor with injection
        /// </summary>
        /// <param name="product">product repository</param>
        /// <param name="productOption">product option repository</param>
        public ProductsController(IProductRepository product, IProductOptionRepository productOption)
        {
            productRepository = product;
            productOptionRepository = productOption;
        }

        /// <summary>
        /// retrieve all products
        /// </summary>
        /// <returns>all products</returns>
        [Route]
        [HttpGet]
        public async Task<ProductListModel> GetAll()
        {
            var model = new ProductListModel { Items = await productRepository.GetAllAsync() };

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
            var model = new ProductListModel { Items = await productRepository.GetByNameAsync(name) };

            return model;
        }

        /// <summary>
        /// retrieve product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>matching product</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<IProduct> GetProduct(Guid id)
        {
            var model = await productRepository.GetByIdAsync(id);

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
        public async Task<HttpResponseMessage> Create(IProduct product)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            await productRepository.CreateAsync(product);

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
        public async Task<HttpResponseMessage> Update(Guid id, IProduct product)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var current = await productRepository.GetByIdAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            current.Name = product.Name;
            current.Description = product.Description;
            current.Price = product.Price;
            current.DeliveryPrice = product.DeliveryPrice;

            await productRepository.UpdateAsync(current);

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
            var current = await productRepository.GetByIdAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            await productRepository.DeleteAsync(current.Id);

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
            var model = new ProductOptionListModel { Items = await productOptionRepository.GetByProductIdAsync(productId) };

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
        public async Task<IProductOption> GetOption(Guid productId, Guid id)
        {
            var model = await productOptionRepository.GetByIdAsync(id);

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
        public async Task<HttpResponseMessage> CreateOption(Guid productId, IProductOption option)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            await productOptionRepository.CreateAsync(productId, option);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        /// <summary>
        /// update product option
        /// </summary>
        /// <param name="id">option id</param>
        /// <param name="option">option</param>
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateOption(Guid id, IProductOption option)
        {
            // validate
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var current = await productOptionRepository.GetByIdAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            current.Name = option.Name;
            current.Description = option.Description;

            await productOptionRepository.UpdateAsync(current);

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
            var current = await productOptionRepository.GetByIdAsync(id);

            if (current == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            await productOptionRepository.DeleteAsync(current.Id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
