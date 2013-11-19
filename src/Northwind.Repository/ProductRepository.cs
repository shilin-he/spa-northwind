using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface IProductRepository : IDisposable
    {
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategoryId(int id);
        IEnumerable<Product> GetProductBySupplierId(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        void DeleteProduct(int id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _ctx;

        public ProductRepository()
        {
            _ctx = new NorthwindContext();
        }

        public Product GetProductById(int id)
        {
            return _ctx.Products.Find(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _ctx.Products.Include("Category").Include("Supplier");
        }

        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            return _ctx.Products.Where(p => p.CategoryId == id);
        }

        public IEnumerable<Product> GetProductBySupplierId(int id)
        {
            return _ctx.Products.Where(p => p.SupplierId == id);
        }

        public Product AddProduct(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            // Load "Category" and "Supplier" to populate 
            // the "CategoryName" and "SupplierCompanyName" properties in ProductDto.
            _ctx.Entry(product).Reference(p => p.Category).Load();
            _ctx.Entry(product).Reference(p => p.Supplier).Load();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _ctx.Entry(product).State = EntityState.Modified;
            _ctx.SaveChanges();
            // Load "Category" and "Supplier" to populate 
            // the "CategoryName" and "SupplierCompanyName" properties in ProductDto.
            _ctx.Entry(product).Reference(p => p.Category).Load();
            _ctx.Entry(product).Reference(p => p.Supplier).Load();
            return product;
        }

        public void DeleteProduct(int id)
        {
            _ctx.Products.Remove(_ctx.Products.Find(id));
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
