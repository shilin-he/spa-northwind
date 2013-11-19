using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.Model;
using Northwind.Repository;
using Northwind.UI.Models;

namespace Northwind.UI.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryRepository _repo;
        private readonly IAdapter _adapter;

        public CategoryController(ICategoryRepository categoryRepository, IAdapter adapter)
        {
            _repo = categoryRepository;
            _adapter = adapter;
        }

        public CategoryController()
        {
            _repo = new CategoryRepository();
            _adapter = new Adapter();
        }

        public IEnumerable<CategoryDto> Get()
        {
            return _adapter.Adapt<IEnumerable<Category>, IEnumerable<CategoryDto>>(_repo.GetCategories());
        }

        // GET api/category/5
        public HttpResponseMessage Get(int id)
        {
            var category = _repo.GetCategoryById(id);
            return category == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found") :
                Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Category, CategoryDto>(category));
        }

        // POST api/category
        public HttpResponseMessage Post([FromBody]CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (categoryDto.TempPictureId != null)
            {
                categoryDto.Picture = _repo.GetCategoryPicture(categoryDto.TempPictureId.ToString());
            }

            var category = _repo.AddCategory(_adapter.Adapt<CategoryDto, Category>(categoryDto));

            var response = Request.CreateResponse(HttpStatusCode.Created, 
                _adapter.Adapt<Category, CategoryDto>(category));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = category.CategoryId }));

            return response;
        }

        // PUT api/category/5
        public HttpResponseMessage Put(int id, [FromBody]CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != categoryDto.CategoryId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (categoryDto.TempPictureId != null)
            {
                categoryDto.Picture = _repo.GetCategoryPicture(categoryDto.TempPictureId.ToString());
            }

            var category = _repo.UpdateCategory(_adapter.Adapt<CategoryDto, Category>(categoryDto));

            return Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Category, CategoryDto>(category));
        }

        // DELETE api/category/5
        public HttpResponseMessage Delete(int id)
        {
            _repo.DeleteCategory(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}