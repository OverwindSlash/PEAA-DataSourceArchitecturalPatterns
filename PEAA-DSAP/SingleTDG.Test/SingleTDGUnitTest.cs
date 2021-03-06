﻿using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SingleTDG.Test
{
    [TestClass]
    public class SingleTDGUnitTest
    {
        [TestMethod]
        public void TestFindOrderDetailById()
        {
            int orderId = 10693;

            IList<OrderDetailDto> orderDetailDtos = NorthwindTDG.FindOrderDetailById(orderId).ToList();

            Assert.AreEqual(4, orderDetailDtos.Count);

            Assert.AreEqual(orderId, orderDetailDtos[0].OrderID);
            Assert.AreEqual(9, orderDetailDtos[0].ProductID);
            Assert.AreEqual(6, orderDetailDtos[0].Quantity);
            Assert.AreEqual(0f, orderDetailDtos[0].Discount);

            Assert.AreEqual(orderId, orderDetailDtos[1].OrderID);
            Assert.AreEqual(54, orderDetailDtos[1].ProductID);
            Assert.AreEqual(60, orderDetailDtos[1].Quantity);
            Assert.AreEqual(0.15f, orderDetailDtos[1].Discount);

            Assert.AreEqual(orderId, orderDetailDtos[2].OrderID);
            Assert.AreEqual(69, orderDetailDtos[2].ProductID);
            Assert.AreEqual(30, orderDetailDtos[2].Quantity);
            Assert.AreEqual(0.15f, orderDetailDtos[2].Discount);

            Assert.AreEqual(orderId, orderDetailDtos[3].OrderID);
            Assert.AreEqual(73, orderDetailDtos[3].ProductID);
            Assert.AreEqual(15, orderDetailDtos[3].Quantity);
            Assert.AreEqual(0.15f, orderDetailDtos[3].Discount);
        }

        [TestMethod]
        public void TestFindProductById()
        {
            int orderId = 10693;

            IList<OrderDetailDto> orderDetailDtos = NorthwindTDG.FindOrderDetailById(orderId).ToList();

            int productId0 = orderDetailDtos[0].ProductID;
            ProductDto productDto0 = NorthwindTDG.FindProductById(productId0).SingleOrDefault();
            Assert.AreEqual(productId0, productDto0.ProductID);
            Assert.AreEqual("Mishi Kobe Niku", productDto0.ProductName);
            Assert.AreEqual(6, productDto0.CategoryID);

            int productId1 = orderDetailDtos[1].ProductID;
            ProductDto productDto1 = NorthwindTDG.FindProductById(productId1).SingleOrDefault();
            Assert.AreEqual(productId1, productDto1.ProductID);
            Assert.AreEqual("Tourtière", productDto1.ProductName);
            Assert.AreEqual(6, productDto1.CategoryID);

            int productId2 = orderDetailDtos[2].ProductID;
            ProductDto productDto2 = NorthwindTDG.FindProductById(productId2).SingleOrDefault();
            Assert.AreEqual(productId2, productDto2.ProductID);
            Assert.AreEqual("Gudbrandsdalsost", productDto2.ProductName);
            Assert.AreEqual(4, productDto2.CategoryID);

            int productId3 = orderDetailDtos[3].ProductID;
            ProductDto productDto3 = NorthwindTDG.FindProductById(productId3).SingleOrDefault();
            Assert.AreEqual(productId3, productDto3.ProductID);
            Assert.AreEqual("Röd Kaviar", productDto3.ProductName);
            Assert.AreEqual(8, productDto3.CategoryID);
        }

        [TestMethod]
        public void TestFindCategoryById()
        {
            int orderId = 10693;

            IList<OrderDetailDto> orderDetailDtos = NorthwindTDG.FindOrderDetailById(orderId).ToList();

            int productId0 = orderDetailDtos[0].ProductID;
            ProductDto productDto0 = NorthwindTDG.FindProductById(productId0).SingleOrDefault();
            CategoryDto categoryDto0 = NorthwindTDG.FindCategoryById(productDto0.CategoryID).SingleOrDefault();
            Assert.AreEqual("Meat/Poultry", categoryDto0.CategoryName);

            int productId1 = orderDetailDtos[1].ProductID;
            ProductDto productDto1 = NorthwindTDG.FindProductById(productId1).SingleOrDefault();
            CategoryDto categoryDto1 = NorthwindTDG.FindCategoryById(productDto1.CategoryID).SingleOrDefault();
            Assert.AreEqual("Meat/Poultry", categoryDto1.CategoryName);

            int productId2 = orderDetailDtos[2].ProductID;
            ProductDto productDto2 = NorthwindTDG.FindProductById(productId2).SingleOrDefault();
            CategoryDto categoryDto2 = NorthwindTDG.FindCategoryById(productDto2.CategoryID).SingleOrDefault();
            Assert.AreEqual("Dairy Products", categoryDto2.CategoryName);

            int productId3 = orderDetailDtos[3].ProductID;
            ProductDto productDto3 = NorthwindTDG.FindProductById(productId3).SingleOrDefault();
            CategoryDto categoryDto3 = NorthwindTDG.FindCategoryById(productDto3.CategoryID).SingleOrDefault();
            Assert.AreEqual("Seafood", categoryDto3.CategoryName);
        }

        [TestMethod]
        public void TestFindOrderRelativeInfoByOrderId()
        {
            int orderId = 10693;

            IList<OrderRelativeInfoDto> orderRelativeInfoDtos =
                NorthwindTDG.FindOrderRelativeInfoByOrderId(orderId).ToList();

            Assert.AreEqual(4, orderRelativeInfoDtos.Count);

            Assert.AreEqual(orderId, orderRelativeInfoDtos[0].OrderID);
            Assert.AreEqual("Mishi Kobe Niku", orderRelativeInfoDtos[0].ProductName);
            Assert.AreEqual(6, orderRelativeInfoDtos[0].CategoryID);
            Assert.AreEqual("Meat/Poultry", orderRelativeInfoDtos[0].CategoryName);
            Assert.AreEqual(6, orderRelativeInfoDtos[0].Quantity);
            Assert.AreEqual(0.0f, orderRelativeInfoDtos[0].Discount);

            Assert.AreEqual(orderId, orderRelativeInfoDtos[1].OrderID);
            Assert.AreEqual("Tourtière", orderRelativeInfoDtos[1].ProductName);
            Assert.AreEqual(6, orderRelativeInfoDtos[1].CategoryID);
            Assert.AreEqual("Meat/Poultry", orderRelativeInfoDtos[1].CategoryName);
            Assert.AreEqual(60, orderRelativeInfoDtos[1].Quantity);
            Assert.AreEqual(0.15f, orderRelativeInfoDtos[1].Discount);

            Assert.AreEqual(orderId, orderRelativeInfoDtos[2].OrderID);
            Assert.AreEqual("Gudbrandsdalsost", orderRelativeInfoDtos[2].ProductName);
            Assert.AreEqual(4, orderRelativeInfoDtos[2].CategoryID);
            Assert.AreEqual("Dairy Products", orderRelativeInfoDtos[2].CategoryName);
            Assert.AreEqual(30, orderRelativeInfoDtos[2].Quantity);
            Assert.AreEqual(0.15f, orderRelativeInfoDtos[2].Discount);

            Assert.AreEqual(orderId, orderRelativeInfoDtos[3].OrderID);
            Assert.AreEqual("Röd Kaviar", orderRelativeInfoDtos[3].ProductName);
            Assert.AreEqual(8, orderRelativeInfoDtos[3].CategoryID);
            Assert.AreEqual("Seafood", orderRelativeInfoDtos[3].CategoryName);
            Assert.AreEqual(15, orderRelativeInfoDtos[3].Quantity);
            Assert.AreEqual(0.15f, orderRelativeInfoDtos[3].Discount);
        }
    }
}
