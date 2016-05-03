using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using SimpleKeyRDG.IdentityMap;

namespace SimpleKeyRDG.Gateway
{
    public class ProductGateway : AbstractGateway
    {
        #region Property
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        #endregion

        public static ProductGateway Load(IDataRecord reader)
        {
            int productId = reader.GetInt32("ProductID");

            ProductGateway productGateway = IdentityMap<int, ProductGateway>.Instance.GetEntry(productId);
            if (productGateway != null)
            {
                return productGateway;
            }


            productGateway = new ProductGateway();
            productGateway.ProductID = reader.GetInt32("ProductID");
            productGateway.ProductName = reader.GetString("ProductName");
            productGateway.SupplierID = reader.GetInt32OrNull("SupplierID");
            productGateway.CategoryID = reader.GetInt32OrNull("CategoryID");
            productGateway.QuantityPerUnit = reader.GetString("QuantityPerUnit");
            productGateway.UnitPrice = reader.GetDecimalOrNull("UnitPrice");
            productGateway.UnitsInStock = reader.GetInt16OrNull("UnitsInStock");
            productGateway.UnitsOnOrder = reader.GetInt16OrNull("UnitsOnOrder");
            productGateway.ReorderLevel = reader.GetInt16OrNull("ReorderLevel");
            productGateway.Discontinued = reader.GetBoolean("Discontinued");

            IdentityMap<int, ProductGateway>.Instance.PutEntry(productGateway.ProductID, productGateway);

            return productGateway;
        }
    }
}
