using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.UI.Models
{
    public class ProductDto
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierCompanyName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}