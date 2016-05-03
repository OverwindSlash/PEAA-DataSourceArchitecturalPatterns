using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleKeyRDG.Gateway;
using SimpleKeyRDG.IdentityMap;

namespace SimpleKeyRDG.Finder
{
    public class ProductFinder : AbstractFinder
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>();

        protected override AbstractGateway TryGetFromIdentityMap(int uniqueId)
        {
            return IdentityMap<int, ProductGateway>.Instance.GetEntry(uniqueId);
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

            return (ProductGateway)FindSingleById(productId);
        }
    }
}
