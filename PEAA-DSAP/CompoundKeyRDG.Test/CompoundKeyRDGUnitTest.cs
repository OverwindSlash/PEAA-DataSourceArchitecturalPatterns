using System;
using System.Collections.Generic;
using CompoundKeyRDG.Finder;
using CompoundKeyRDG.Gateway;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompoundKeyRDG.Test
{
    [TestClass]
    public class CompoundKeyRDGUnitTest
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

        [TestMethod]
        public void TestOrderDetailFinderWithUniqueId()
        {
            int orderId = 10693;
            int productId = 9;

            OrderDetailFinder orderDetailFinder = new OrderDetailFinder();

            OrderDetailGateway orderDetiDetailGateway =
                orderDetailFinder.FindOrderDetailGatewayByIds(orderId, productId);

            Assert.AreEqual(orderId, orderDetiDetailGateway.OrderID);
            Assert.AreEqual(9, orderDetiDetailGateway.ProductID);
            Assert.AreEqual(6, orderDetiDetailGateway.Quantity);
            Assert.AreEqual(0f, orderDetiDetailGateway.Discount);
        }
    }
}
