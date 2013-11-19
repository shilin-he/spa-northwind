using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.Model;
using Northwind.Repository;
using Northwind.UI.Models;

namespace Northwind.UI.Controllers
{
    public class SupplierController : ApiController
    {
         
        private readonly SupplierRepository _repo;
        private readonly IAdapter _adapter;

        public SupplierController(SupplierRepository supplierRepository, IAdapter adapter)
        {
            _repo = supplierRepository;
            _adapter = adapter;
        }

        public SupplierController()
        {
            _repo = new SupplierRepository();
            _adapter = new Adapter();
        }

        public IEnumerable<SupplierDto> Get()
        {
            return _adapter.Adapt<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(_repo.GetSuppliers());
        }

        // GET api/supplier/5
        public HttpResponseMessage Get(int id)
        {
            var supplier = _repo.GetSupplierById(id);
            return supplier == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found") :
                Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Supplier, SupplierDto>(supplier));
        }

        // POST api/supplier
        public HttpResponseMessage Post([FromBody]SupplierDto supplier)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            supplier.SupplierId= _repo.AddSupplier(_adapter.Adapt<SupplierDto, Supplier>(supplier)).SupplierId;

            var response = Request.CreateResponse(HttpStatusCode.Created, supplier);
            response.Headers.Location = new Uri(Url.Link(routeName: "DefaultApi", routeValues: new { id = supplier.SupplierId }));

            return response;
        }

        // PUT api/supplier/5
        public HttpResponseMessage Put(int id, [FromBody]SupplierDto supplier)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != supplier.SupplierId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _repo.UpdateSupplier(_adapter.Adapt<SupplierDto, Supplier>(supplier));

            return Request.CreateResponse(HttpStatusCode.OK, supplier);
        }

        // DELETE api/supplier/5
        public HttpResponseMessage Delete(int id)
        {
            _repo.DeleteSupplier(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}