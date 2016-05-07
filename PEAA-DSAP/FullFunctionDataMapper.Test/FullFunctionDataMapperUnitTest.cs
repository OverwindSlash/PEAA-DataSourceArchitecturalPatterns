using System;
using FullFunctionDataMapper.CompoundKey;
using FullFunctionDataMapper.Domain;
using FullFunctionDataMapper.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FullFunctionDataMapper.Test
{
    [TestClass]
    public class FullFunctionDataMapperUnitTest
    {
        [TestMethod]
        public void TestProductFinder()
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

        [TestMethod]
        public void TestIdentityMapOfProductFinder()
        {
            int productId = 9;
            int anotherProductId = 54;

            Key key1 = new Key(productId);
            Key key2 = new Key(productId);

            Assert.AreEqual(key1, key2);

            ProductMapper productMapper = new ProductMapper();

            Product product1 = productMapper.FindProductById(productId);
            Product product2 = productMapper.FindProductById(productId);

            Assert.AreEqual(product1, product2);

            Product product3 = productMapper.FindProductById(anotherProductId);
            Assert.AreNotEqual(product1, product3);
            Assert.AreNotEqual(product2, product3);
        }
    }
}
