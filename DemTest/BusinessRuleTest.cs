using System;
using AnagramChecker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemTest
{
    [TestClass]
    public class BusinessRuleTest
    {
        [TestMethod]
        public void Test_Biz_Rule_Different_Product_Types()
        {
            IProductAbstractFactory physicalFactory = new PhysicalProductFactory();
            IProductAbstractFactory bookFactory = new BookFactory();
            IProduct physicalProduct, book;
            physicalProduct = physicalFactory.GetProduct();
            book = bookFactory.GetProduct();
            Assert.AreEqual(ProductType.PhysicalProduct, physicalProduct.Type);
            Assert.AreEqual(ProductType.Book, book.Type);
        }
    }
}
