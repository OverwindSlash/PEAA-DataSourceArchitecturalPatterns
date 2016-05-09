using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using GenericQueryDataMapper.CompoundKey;
using GenericQueryDataMapper.Domain;
using GenericQueryDataMapper.IdentityMap;
using GenericQueryDataMapper.Statement;

namespace GenericQueryDataMapper.Mapper
{
    public class ProductMapper : AbstractMapper
    {
        protected override DomainObject TryGetFromIdentityMap(Key uniqueKey)
        {
            return IdentityMap<Key, Product>.Instance.GetEntry(uniqueKey);
        }

        protected override DomainObject MapRecordToDomainObject(IDataRecord record)
        {
            // Generate Unique Key.
            int productId = record.GetInt32("ProductID");
            Key productKey = new Key(productId);

            // Check Identity Map.
            Product product = IdentityMap<Key, Product>.Instance.GetEntry(productKey);
            if (product != null)
            {
                return product;
            }

            // Create Domain Object.
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

            // Add created Domain Object to Identity Map.
            IdentityMap<Key, Product>.Instance.PutEntry(productKey, product);

            return product;
        }


        public Product FindProductById(int productId)
        {
            Key productKey = new Key(productId);
            StatementSource statementSource = new StatementSource();
            statementSource.Statement =
                "SELECT * FROM [Products] " +
                "WHERE [Products].ProductID = @ProductID";
            statementSource.Parameters = CreateFindProductByIdParam(productId);

            return (Product)FindSingleById(productKey, statementSource);
        }

        private IList<IDbDataParameter> CreateFindProductByIdParam(int productId)
        {
            IList<IDbDataParameter> parameters = new List<IDbDataParameter>();

            DbParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@ProductID";
            parameter.DbType = DbType.Int32;
            parameter.Value = productId;

            parameters.Add(parameter);

            return parameters;
        }


        public IList<Product> FindProductsWithCategoryId(int categoryId)
        {
            StatementSource statementSource = new StatementSource();
            statementSource.Statement =
                "SELECT * FROM [Products] " +
                "WHERE [Products].CategoryID = @CategoryID";
            statementSource.Parameters = CreateFindProductsWithCategoryIdParam(categoryId);

            return FindDomainObjectsByCriteria(statementSource).Cast<Product>().ToList();
        }

        private IList<IDbDataParameter> CreateFindProductsWithCategoryIdParam(int categoryId)
        {
            IList<IDbDataParameter> parameters = new List<IDbDataParameter>();

            DbParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@CategoryID";
            parameter.DbType = DbType.Int32;
            parameter.Value = categoryId;

            parameters.Add(parameter);

            return parameters;
        }
    }
}
