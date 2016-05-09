using System;
using System.Collections.Generic;
using GenericQueryDataMapper.CompoundKey;
using GenericQueryDataMapper.Domain;
using GenericQueryDataMapper.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericQueryDataMapper.Test
{
    [TestClass]
    public class GenericQueryDataMapperUnitTest
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
                Assert.AreEqual((short) 29, product.UnitsInStock);
                Assert.AreEqual((short) 0, product.UnitsOnOrder);
                Assert.AreEqual((short) 0, product.ReorderLevel);
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

            [TestMethod]
            public void TestQueryProductWithCategoryId()
            {
                int categoryId = 6;

                ProductMapper productMapper = new ProductMapper();
                IList<Product> products = productMapper.FindProductsWithCategoryId(categoryId);

                Assert.AreEqual(6, products.Count);

                Assert.AreEqual(9, products[0].ProductID);
                Assert.AreEqual("Mishi Kobe Niku", products[0].ProductName);
                Assert.AreEqual(4, products[0].SupplierID);
                Assert.AreEqual(6, products[0].CategoryID);
                Assert.AreEqual("18 - 500 g pkgs.", products[0].QuantityPerUnit);
                Assert.AreEqual(97.0000m, products[0].UnitPrice);
                Assert.AreEqual((short) 29, products[0].UnitsInStock);
                Assert.AreEqual((short) 0, products[0].UnitsOnOrder);
                Assert.AreEqual((short) 0, products[0].ReorderLevel);
                Assert.AreEqual(true, products[0].Discontinued);


                Assert.AreEqual(17, products[1].ProductID);
                Assert.AreEqual(29, products[2].ProductID);
                Assert.AreEqual(53, products[3].ProductID);
                Assert.AreEqual(54, products[4].ProductID);
                Assert.AreEqual(55, products[5].ProductID);
            }

            [TestMethod]
            public void TestIdentityMapOfQueryProductWithCategoryId()
            {
                int categoryId = 6;

                ProductMapper productMapper = new ProductMapper();
                IList<Product> products = productMapper.FindProductsWithCategoryId(categoryId);
                IList<Product> anotherProducts = productMapper.FindProductsWithCategoryId(categoryId);

                for (int i = 0; i < products.Count; i++)
                {
                    Assert.AreEqual(products[i], anotherProducts[i]);
                }
            }
        }
    }
}
