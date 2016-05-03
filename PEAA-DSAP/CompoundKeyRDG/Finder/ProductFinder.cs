using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompoundKeyRDG.CompoundKey;
using CompoundKeyRDG.Gateway;
using CompoundKeyRDG.IdentityMap;

namespace CompoundKeyRDG.Finder
{
    public class ProductFinder : AbstractFinder
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>();

        protected override AbstractGateway TryGetFromIdentityMap(Key uniqueKey)
        {
            return IdentityMap<Key, ProductGateway>.Instance.GetEntry(uniqueKey);
        }

        protected override string GetExecuteReaderSql()
        {
            return "SELECT * FROM [Products] " +
                   "WHERE [Products].ProductID = @ProductID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@ProductID";
            parameter.DbType = DbType.Int32;
            parameter.Value = parameters[parameter.ParameterName];
            command.Parameters.Add(parameter);
        }

        protected override AbstractGateway CreateGateway(IDataRecord reader)
        {
            return ProductGateway.Load(reader);
        }

        public ProductGateway FindProductGatewayById(int productId)
        {
            parameters["@ProductID"] = productId;

            Key productKey = new Key(productId);

            return (ProductGateway)FindSingleById(productKey);
        }
    }
}
