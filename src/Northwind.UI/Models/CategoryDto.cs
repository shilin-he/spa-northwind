using System;

namespace Northwind.UI.Models
{
    public class CategoryDto
    {
        public int? CategoryId { get; set; }
        public Guid? PictureId { get; set; }
        public byte[] Picture { get; set; }
        public Guid? TempPictureId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        // True if no products in the category, otherwise false.
        public bool IsDeletable { get; set; }
    }
}