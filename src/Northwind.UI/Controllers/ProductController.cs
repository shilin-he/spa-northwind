using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Northwind.Model;
using Northwind.Repository;
using Northwind.UI.Models;

namespace Northwind.UI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductRepository _repo;
        private readonly IAdapter _adapter;

        public ProductController(IProductRepository productRepository, IAdapter adapter)
        {
            _repo = productRepository;
            _adapter = adapter;
        }

        public ProductController()
        {
            _repo = new ProductRepository();
            _adapter = new Adapter();
        }

        public IEnumerable<ProductDto> Get()
        {
            return _adapter.Adapt<IEnumerable<Product>, IEnumerable<ProductDto>>(_repo.GetProducts());
        }

        // GET api/product/5
        public HttpResponseMessage Get(int id)
        {
            var product = _repo.GetProductById(id);
            return product == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found") :
                Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Product, ProductDto>(product));
        }

        // POST api/productDto
        public HttpResponseMessage Post([FromBody]ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var productModel = _repo.AddProduct(_adapter.Adapt<ProductDto, Product>(productDto));

            var response = Request.CreateResponse(
                HttpStatusCode.Created, _adapter.Adapt<Product, ProductDto>(productModel));
            response.Headers.Location = new Uri(Url.Link(routeName: "DefaultApi", routeValues: new { id = productDto.ProductId }));

            return response;
        }

        // PUT api/productDto/5
        public HttpResponseMessage Put(int id, [FromBody]ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != productDto.ProductId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var product = _repo.UpdateProduct(_adapter.Adapt<ProductDto, Product>(productDto));

            return Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Product, ProductDto>(product));
        }

        // DELETE api/product/5
        public HttpResponseMessage Delete(int id)
        {
            _repo.DeleteProduct(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}