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
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _repo;
        private readonly IAdapter _adapter;

        public OrderController(IOrderRepository orderRepository, IAdapter adapter)
        {
            _repo = orderRepository;
            _adapter = adapter;
        }

        public OrderController()
        {
            _repo = new OrderRepository();
            _adapter = new Adapter();
        }

        public IEnumerable<OrderDto> Get()
        {
            return _adapter.Adapt<IEnumerable<Order>, IEnumerable<OrderDto>>(_repo.GetOrders());
        }

        // GET api/order/5
        public HttpResponseMessage Get(int id)
        {
            var order = _repo.GetOrderById(id);
            return order == null ?
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found") :
                Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Order, OrderDto>(order));
        }

        // POST api/orderDto
        public HttpResponseMessage Post([FromBody]OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var orderModel = _repo.AddOrder(_adapter.Adapt<OrderDto, Order>(orderDto));

            var response = Request.CreateResponse(
                HttpStatusCode.Created, _adapter.Adapt<Order, OrderDto>(orderModel));
            response.Headers.Location = new Uri(Url.Link(routeName: "DefaultApi", routeValues: new { id = orderDto.OrderId }));

            return response;
        }

        // PUT api/orderDto/5
        public HttpResponseMessage Put(int id, [FromBody]OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != orderDto.OrderId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var order = _repo.UpdateOrder(_adapter.Adapt<OrderDto, Order>(orderDto));

            return Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Order, OrderDto>(order));
        }

        // DELETE api/order/5
        public HttpResponseMessage Delete(int id)
        {
            _repo.DeleteOrder(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}