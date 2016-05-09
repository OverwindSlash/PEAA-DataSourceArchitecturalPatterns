using System;
using System.Collections.Generic;
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
        public void TestInsertProductWithUoW()
        {
            Product product = new Product();
            product.ProductName = "Test Product";
            product.Discontinued = false;

            MapperRegistry.Instance.Add(typeof (Product), ProductMapper.Instance);

            ProductMapper mapper = (ProductMapper)MapperRegistry.Instance.Get(typeof (Product));
            mapper.Attach(product);
            mapper.SaveChanges();
        }
    }
}
