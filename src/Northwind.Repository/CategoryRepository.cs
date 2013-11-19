using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(int id);
        byte[] GetCategoryPicture(string fileName);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext _ctx;

        public CategoryRepository()
        {
            _ctx = new NorthwindContext();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _ctx.Categories.Include("Products");
        }

        public Category GetCategoryById(int id)
        {
            return _ctx.Categories.Find(id);
        }

        public Category AddCategory(Category category)
        {
            _ctx.Categories.Add(category);
            _ctx.SaveChanges();
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            _ctx.Entry(category).State = EntityState.Modified;
            _ctx.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            _ctx.Categories.Remove(_ctx.Categories.Find(id));
            _ctx.SaveChanges();
        }

        public byte[] GetCategoryPicture(string fileName)
        {
            var appDataPath = HttpContext.Current.Server.MapPath("~/App_Data");
            var pictureFilePath = Path.Combine(appDataPath, fileName);
            var picture = File.ReadAllBytes(pictureFilePath);
            File.Delete(pictureFilePath);
            return picture;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}