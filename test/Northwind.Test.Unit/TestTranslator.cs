using AutoMapper;
using NUnit.Framework;
using Northwind.UI.Models;

namespace Northwind.Test.Unit
{
    [TestFixture]
    public class TranslatorTest
    {
        [Test]
        public void TestAutoMapperConfiguration()
        {
            Adapter.Init();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
