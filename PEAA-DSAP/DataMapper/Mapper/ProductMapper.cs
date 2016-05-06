using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.CompoundKey;
using DataMapper.Domain;
using DataMapper.IdentityMap;
using DbUtil;

namespace DataMapper.Mapper
{
    public class ProductMapper : AbstractMapper
    {
        protected override string FindSingleStatement()
        {
            return "SELECT * FROM [Products] " +
                   "WHERE [Products].ProductID = @ProductID";
        }

        protected override DomainObject TryGetFromIdentityMap(Key uniqueKey)
        {
            return IdentityMap<Key, Product>.Instance.GetEntry(uniqueKey);
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@ProductID";
            parameter.DbType = DbType.Int32;
            parameter.Value = parameters[parameter.ParameterName];
            command.Parameters.Add(parameter);
        }

        protected override DomainObject MapRecordToDomainObject(IDataRecord record)
        {
            int productId = record.GetInt32("ProductID");

            Key productKey = new Key(productId);

            Product product = IdentityMap<Key, Product>.Instance.GetEntry(productKey);
            if (product != null)
            {
                return product;
            }


            product = new Product();
            product.ProductID = record.GetInt32("ProductID");
            product.ProductName = record.GetString("ProductName");
            product.SupplierID = record.GetInt32OrNull("SupplierID");
            product.CategoryID = record.GetInt32OrNull("CategoryID");
            product.QuantityPerUnit = record.GetString("QuantityPerUnit");
            product.UnitPrice = record.GetDecimalOrNull("UnitPrice");
            product.UnitsInStock = record.GetInt16OrNull("UnitsInStock");
            product.UnitsOnOrder = record.GetInt16OrNull("UnitsOnOrder");
            product.ReorderLevel = record.GetInt16OrNull("ReorderLevel");
            product.Discontinued = record.GetBoolean("Discontinued");

            IdentityMap<Key, Product>.Instance.PutEntry(productKey, product);

            return product;
        }

        public Product FindProductById(int productId)
        {
            parameters["@ProductID"] = productId;

            Key productKey = new Key(productId);

            return (Product)FindSingleById(productKey);
        }
    }
}
