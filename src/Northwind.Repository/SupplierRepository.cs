using System;
using System.Collections.Generic;
using System.Data;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface ISupplierRepository : IDisposable
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplierById(int id);
        Supplier AddSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(int id);
    }

    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindContext _ctx;

        public SupplierRepository()
        {
            _ctx = new NorthwindContext();
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _ctx.Suppliers;
        }

        public Supplier GetSupplierById(int id)
        {
            return _ctx.Suppliers.Find(id);
        }

        public Supplier AddSupplier(Supplier supplier)
        {
            _ctx.Suppliers.Add(supplier);
            _ctx.SaveChanges();
            return supplier;
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _ctx.Entry(supplier).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            _ctx.Categories.Remove(_ctx.Categories.Find(id));
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}