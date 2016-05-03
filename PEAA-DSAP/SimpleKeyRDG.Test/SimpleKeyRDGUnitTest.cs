using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleKeyRDG.Finder;
using SimpleKeyRDG.Gateway;
using System.Drawing;

namespace SimpleKeyRDG.Test
{
    [TestClass]
    public class SimpleKeyRDGUnitTest
    {
        [TestMethod]
        public void TestProductFinder()
        {
            int productId = 9;

            ProductFinder productFinder = new ProductFinder();
            ProductGateway productGateway = productFinder.FindProductGatewayById(productId);

            Assert.AreEqual(productId, productGateway.ProductID);
            Assert.AreEqual("Mishi Kobe Niku", productGateway.ProductName);
            Assert.AreEqual(4, productGateway.SupplierID);
            Assert.AreEqual(6, productGateway.CategoryID);
            Assert.AreEqual("18 - 500 g pkgs.", productGateway.QuantityPerUnit);
            Assert.AreEqual(97.0000m, productGateway.UnitPrice);
            Assert.AreEqual((short)29, productGateway.UnitsInStock);
            Assert.AreEqual((short)0, productGateway.UnitsOnOrder);
            Assert.AreEqual((short)0, productGateway.ReorderLevel);
            Assert.AreEqual(true, productGateway.Discontinued);
        }

        [TestMethod]
        public void TestIdentityMapOfProductFinder()
        {
            int productId = 9;
            int anotherProductId = 54;

            ProductFinder productFinder = new ProductFinder();
            ProductGateway productGateway1 = productFinder.FindProductGatewayById(productId);
            ProductGateway productGateway2 = productFinder.FindProductGatewayById(productId);

            Assert.AreEqual(productGateway1, productGateway2);

            ProductGateway productGateway3 = productFinder.FindProductGatewayById(anotherProductId);
            Assert.AreNotEqual(productGateway1, productGateway3);
            Assert.AreNotEqual(productGateway2, productGateway3);
        }

        [TestMethod]
        public void TestCategoryFinder()
        {
            int categoryId = 6;

            CategoryFinder categoryFinder = new CategoryFinder();
            CategoryGateway categoryGateway = categoryFinder.FindCategoryGatewayById(categoryId);

            Assert.AreEqual(categoryId, categoryGateway.CategoryID);
            Assert.AreEqual("Meat/Poultry", categoryGateway.CategoryName);
            Assert.AreEqual("Prepared meats", categoryGateway.Description);
        }

        [TestMethod]
        public void TestIdentityMapOfCategoryFinder()
        {
            int categoryId = 6;
            int anotherCategoryId = 8;

            CategoryFinder categoryFinder = new CategoryFinder();
            CategoryGateway categoryGateway1 = categoryFinder.FindCategoryGatewayById(categoryId);
            CategoryGateway categoryGateway2 = categoryFinder.FindCategoryGatewayById(categoryId);

            Assert.AreEqual(categoryGateway1, categoryGateway2);

            CategoryGateway categoryGateway3 = categoryFinder.FindCategoryGatewayById(anotherCategoryId);
            Assert.AreNotEqual(categoryGateway1, categoryGateway3);
            Assert.AreNotEqual(categoryGateway2, categoryGateway3);
        }

        [TestMethod]
        public void TestOrderDetailFinder()
        {
            int orderId = 10693;

            OrderDetailFinder orderDetailFinder = new OrderDetailFinder();

            IList<OrderDetailGateway> orderDetiDetailGateways =
                orderDetailFinder.FindOrderDetailGatewayByOrderId(orderId);

            Assert.AreEqual(4, orderDetiDetailGateways.Count);

            Assert.AreEqual(orderId, orderDetiDetailGateways[0].OrderID);
            Assert.AreEqual(9, orderDetiDetailGateways[0].ProductID);
            Assert.AreEqual(6, orderDetiDetailGateways[0].Quantity);
            Assert.AreEqual(0f, orderDetiDetailGateways[0].Discount);

            Assert.AreEqual(orderId, orderDetiDetailGateways[1].OrderID);
            Assert.AreEqual(54, orderDetiDetailGateways[1].ProductID);
            Assert.AreEqual(60, orderDetiDetailGateways[1].Quantity);
            Assert.AreEqual(0.15f, orderDetiDetailGateways[1].Discount);

            Assert.AreEqual(orderId, orderDetiDetailGateways[2].OrderID);
            Assert.AreEqual(69, orderDetiDetailGateways[2].ProductID);
            Assert.AreEqual(30, orderDetiDetailGateways[2].Quantity);
            Assert.AreEqual(0.15f, orderDetiDetailGateways[2].Discount);

            Assert.AreEqual(orderId, orderDetiDetailGateways[3].OrderID);
            Assert.AreEqual(73, orderDetiDetailGateways[3].ProductID);
            Assert.AreEqual(15, orderDetiDetailGateways[3].Quantity);
            Assert.AreEqual(0.15f, orderDetiDetailGateways[3].Discount);
        }
    }
}
