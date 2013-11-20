using AutoMapper;
using NUnit.Framework;
using Northwind.UI.Models;

namespace Northwind.Test.Unit
{
    [TestFixture]
    public class AdapterTest
    {
        [Test]
        public void TestAutoMapperConfiguration()
        {
            Adapter.Init();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
