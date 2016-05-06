using System;
using DataMapper.Domain;
using DataMapper.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMapper.Test
{
    [TestClass]
    public class DataMapperUnitTest
    {
        [TestMethod]
        public void TestFindProduct()
        {
            int productId = 9;

            ProductMapper productMapper = new ProductMapper();
            Product product = productMapper.FindProductById(productId);

            Assert.AreEqual(productId, product.ProductID);
            Assert.AreEqual("Mishi Kobe Niku", product.ProductName);
            Assert.AreEqual(4, product.SupplierID);
            Assert.AreEqual(6, product.CategoryID);
            Assert.AreEqual("18 - 500 g pkgs.", product.QuantityPerUnit);
            Assert.AreEqual(97.0000m, product.UnitPrice);
            Assert.AreEqual((short)29, product.UnitsInStock);
            Assert.AreEqual((short)0, product.UnitsOnOrder);
            Assert.AreEqual((short)0, product.ReorderLevel);
            Assert.AreEqual(true, product.Discontinued);
        }
    }
}
