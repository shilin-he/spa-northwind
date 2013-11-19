using System;
using System.Collections.Generic;

namespace Northwind.Model
{
    public sealed class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Guid? PictureId { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
