using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface IOrderDetailRepository : IDisposable
    {
        OrderDetail GetOrderDetail(int orderId, int productId);
        IEnumerable<OrderDetail> GetOrderDetails(int orderId);
        OrderDetail AddOrderDetail(OrderDetail orderDetai);
        OrderDetail UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int orderId);
    }

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly NorthwindContext _ctx;

        public OrderDetailRepository()
        {
            _ctx = new NorthwindContext();
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _ctx.OrderDetails.Find(orderId, productId);
        }

        public IEnumerable<OrderDetail> GetOrderDetails(int orderId)
        {
            return _ctx.OrderDetails.Where(od => od.OrderId == orderId);
        }

        public OrderDetail AddOrderDetail(OrderDetail orderDetai)
        {
            _ctx.OrderDetails.Add(orderDetai);
            _ctx.SaveChanges();
            return orderDetai;
        }

        public OrderDetail UpdateOrderDetail(OrderDetail orderDetail)
        {
            _ctx.Entry(orderDetail).State = EntityState.Modified;
            _ctx.SaveChanges();
            return orderDetail;
        }

        public void DeleteOrderDetail(int orderId)
        {
            _ctx.OrderDetails.Remove(_ctx.OrderDetails.FirstOrDefault(od => od.OrderId == orderId));
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
