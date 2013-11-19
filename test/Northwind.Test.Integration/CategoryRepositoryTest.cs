using System.Linq;
using NUnit.Framework;
using Northwind.Model;
using Northwind.Repository;

namespace Northwind.Test.Integration
{
    [TestFixture]
    public class CategoryRepositoryTest
    {
        private CategoryRepository _repo;

        [SetUp]
        public void SetUp()
        {
            _repo = new CategoryRepository();
        }

        [Test]
        public void GetCategories_ShouldReturnCategories()
        {
            var categories = _repo.GetCategories();

            Assert.That(categories.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void AddCategory_ShouldAddCategory()
        {
            var category = new Category
                {
                    CategoryName = "Test Category"
                };

            _repo.AddCategory(category);

            Assert.That(category.CategoryId, Is.GreaterThan(1));
        }
    }
}