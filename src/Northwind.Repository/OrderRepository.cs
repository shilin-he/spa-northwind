using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Order GetOrderById(int id);
        IEnumerable<Order> GetOrders();
        Order AddOrder(Order order);
        Order UpdateOrder(Order order);
        void DeleteOrder(int id);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _ctx;

        public OrderRepository()
        {
            _ctx = new NorthwindContext();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _ctx.Orders;
        }

        public Order AddOrder(Order order)
        {
            _ctx.Orders.Add(order);
            _ctx.SaveChanges();
            return order;
        }

        public Order UpdateOrder(Order order)
        {
            _ctx.Entry(order).State = EntityState.Modified;
            _ctx.SaveChanges();
            return order;
        }

        public void DeleteOrder(int id)
        {
            _ctx.Orders.Remove(_ctx.Orders.Find(id));
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
