using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using FullFunctionDataMapper.CompoundKey;
using FullFunctionDataMapper.Domain;
using FullFunctionDataMapper.IdentityMap;
using FullFunctionDataMapper.Statement;
using FullFunctionDataMapper.UoW;

namespace FullFunctionDataMapper.Mapper
{
    public sealed class ProductMapper : AbstractMapper
    {
        private static readonly ProductMapper instance = new ProductMapper();

        static ProductMapper() { }

        private ProductMapper() { }

        public static ProductMapper Instance
        {
            get { return instance; }
        }

        #region Overrider
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
        #endregion


        #region Find Single
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
        #endregion


        #region Find Many
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
        #endregion

        public void Attach(Product product)
        {
            UnitOfWork.Instance.RegisterNew(product);
        }

        public void Detach(Product product)
        {
            UnitOfWork.Instance.RegisterClean(product);
        }

        public void InsertProduct(Product product)
        {
            StatementSource statementSource = new StatementSource();
            statementSource.Statement =
                "INSERT INTO [Products] " +
                "([ProductName], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued]) " +
                "VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)";
            statementSource.Parameters = CreateInsertParam(product);

            ExecuteNonQuery(statementSource);
        }

        private IList<IDbDataParameter> CreateInsertParam(Product product)
        {
            IList<IDbDataParameter> parameters = new List<IDbDataParameter>();

            DbParameter parameter1 = providerFactory.CreateParameter();
            parameter1.ParameterName = "@ProductName";
            parameter1.DbType = DbType.String;
            parameter1.Value = product.ProductName;
            parameters.Add(parameter1);

            DbParameter parameter2 = providerFactory.CreateParameter();
            parameter2.ParameterName = "@SupplierID";
            parameter2.DbType = DbType.Int32;
            parameter2.Value = DBNull.Value;
            if (product.SupplierID.HasValue)
            {
                parameter2.Value = product.SupplierID.Value;
            }
            parameters.Add(parameter2);

            DbParameter parameter3 = providerFactory.CreateParameter();
            parameter3.ParameterName = "@CategoryID";
            parameter3.DbType = DbType.Int32;
            parameter3.Value = DBNull.Value;
            if (product.CategoryID.HasValue)
            {
                parameter3.Value = product.CategoryID.Value;
            }
            parameters.Add(parameter3);

            DbParameter parameter4 = providerFactory.CreateParameter();
            parameter4.ParameterName = "@QuantityPerUnit";
            parameter4.DbType = DbType.String;
            parameter4.Value = DBNull.Value;
            if (!string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                parameter4.Value = product.QuantityPerUnit;
            }
            parameters.Add(parameter4);

            DbParameter parameter5 = providerFactory.CreateParameter();
            parameter5.ParameterName = "@UnitPrice";
            parameter5.DbType = DbType.Decimal;
            parameter5.Value = DBNull.Value;
            if (product.UnitPrice.HasValue)
            {
                parameter5.Value = product.UnitPrice.Value;
            }
            parameters.Add(parameter5);

            DbParameter parameter6 = providerFactory.CreateParameter();
            parameter6.ParameterName = "@UnitsInStock";
            parameter6.DbType = DbType.Int16;
            parameter6.Value = DBNull.Value;
            if (product.UnitsInStock.HasValue)
            {
                parameter6.Value = product.UnitsInStock.Value;
            }
            parameters.Add(parameter6);

            DbParameter parameter7 = providerFactory.CreateParameter();
            parameter7.ParameterName = "@UnitsOnOrder";
            parameter7.DbType = DbType.Int16;
            parameter7.Value = DBNull.Value;
            if (product.UnitsOnOrder.HasValue)
            {
                parameter7.Value = product.UnitsOnOrder.Value;
            }
            parameters.Add(parameter7);

            DbParameter parameter8 = providerFactory.CreateParameter();
            parameter8.ParameterName = "@ReorderLevel";
            parameter8.DbType = DbType.Int16;
            parameter8.Value = DBNull.Value;
            if (product.ReorderLevel.HasValue)
            {
                parameter8.Value = product.ReorderLevel.Value;
            }
            parameters.Add(parameter8);

            DbParameter parameter9 = providerFactory.CreateParameter();
            parameter9.ParameterName = "@Discontinued";
            parameter9.DbType = DbType.Boolean;
            parameter9.Value = product.Discontinued;
            parameters.Add(parameter9);

            return parameters;
        }

        public override void Insert(DomainObject domainObject)
        {
            InsertProduct((Product)domainObject);
        }
    }
}
