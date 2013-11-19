using System.Linq;
using NUnit.Framework;
using Northwind.Model;
using Northwind.Repository;

namespace Northwind.Test.Integration
{
    [TestFixture]
    public class ProductRepositoryTest
    {
        private ProductRepository _repo;

        [SetUp]
        public void SetUp()
        {
           _repo = new ProductRepository(); 
        }

        [Test]
        public void GetProducts_ShouldReturnProducts()
        {
            var products = _repo.GetProducts();
            
            Assert.That(products.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void AddProduct_ShouldAddProduct()
        {
            var product = _repo.AddProduct(new Product
                {
                    ProductName = "Test Product 1234"
                });

            Assert.That(product.ProductId, Is.GreaterThan(1));
        }
    }
}
