using System;
using System.Linq;
using NUnit.Framework;
using Northwind.Model;
using Northwind.Repository;

namespace Northwind.Test.Integration
{
    [TestFixture]
    public class OrderRepositoryTest
    {
        private OrderRepository _repo;

        [SetUp]
        public void SetUp()
        {
           _repo = new OrderRepository(); 
        }

        [Test]
        public void GetOrders_ShouldReturnOrders()
        {
            var orders = _repo.GetOrders();
            
            Assert.That(orders.Count(), Is.GreaterThan(0));
        }

        [Ignore]
        [Test]
        public void AddOrder_ShouldAddOrder()
        {
            var product = _repo.AddOrder(new Order
                {
                    OrderDate = DateTime.Today
                });

            Assert.That(product.OrderId, Is.GreaterThan(1));
        }
    }
}