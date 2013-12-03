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
    public class ShipperController : ApiController
    {
        private readonly IShipperRepository _repo;
        private readonly IAdapter _adapter;

        public ShipperController(IShipperRepository shipperRepository, IAdapter adapter)
        {
            _repo = shipperRepository;
            _adapter = adapter;
        }

        public ShipperController()
        {
            _repo = new ShipperRepository();
            _adapter = new Adapter();
        }

        public IEnumerable<ShipperDto> Get()
        {
            return _adapter.Adapt<IEnumerable<Shipper>, IEnumerable<ShipperDto>>(_repo.GetShippers());
        }

        // GET api/shipper/5
        public HttpResponseMessage Get(int id)
        {
            var shipper = _repo.GetShipperById(id);
            return shipper == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found") :
                Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Shipper, ShipperDto>(shipper));
        }

        // POST api/shipperDto
        public HttpResponseMessage Post([FromBody]ShipperDto shipperDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var shipperModel = _repo.AddShipper(_adapter.Adapt<ShipperDto, Shipper>(shipperDto));

            var response = Request.CreateResponse(
                HttpStatusCode.Created, _adapter.Adapt<Shipper, ShipperDto>(shipperModel));
            response.Headers.Location = new Uri(Url.Link(routeName: "DefaultApi", routeValues: new { id = shipperDto.ShipperId }));

            return response;
        }

        // PUT api/shipperDto/5
        public HttpResponseMessage Put(int id, [FromBody]ShipperDto shipperDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != shipperDto.ShipperId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var shipper = _repo.UpdateShipper(_adapter.Adapt<ShipperDto, Shipper>(shipperDto));

            return Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Shipper, ShipperDto>(shipper));
        }

        // DELETE api/shipper/5
        public HttpResponseMessage Delete(int id)
        {
            _repo.DeleteShipper(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}