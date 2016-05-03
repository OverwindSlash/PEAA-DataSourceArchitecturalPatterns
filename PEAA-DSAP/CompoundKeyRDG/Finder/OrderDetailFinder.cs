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
    public class OrderDetailFinder : AbstractFinder
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>();

        protected override AbstractGateway TryGetFromIdentityMap(Key uniqueKey)
        {
            return IdentityMap<Key, OrderDetailGateway>.Instance.GetEntry(uniqueKey);
        }

        protected override string GetExecuteReaderSql()
        {
            return "SELECT * FROM [Order Details] " +
                   "WHERE [Order Details].OrderID = @OrderID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@OrderID";
            parameter.DbType = DbType.Int32;
            parameter.Value = parameters[parameter.ParameterName];
            command.Parameters.Add(parameter);
        }

        protected override AbstractGateway CreateGateway(IDataRecord reader)
        {
            return OrderDetailGateway.Load(reader);
        }

        public IList<OrderDetailGateway> FindOrderDetailGatewayByOrderId(int orderId)
        {
            parameters["@OrderID"] = orderId;

            return FindMultiByCriteria().Cast<OrderDetailGateway>().ToList();
        }

        public OrderDetailGateway FindOrderDetailGatewayByIds(int orderId, int productId)
        {
            parameters["@OrderID"] = orderId;
            parameters["@ProductID"] = productId;

            Key orderDetailKey = new Key(orderId, productId);

            return (OrderDetailGateway)FindSingleById(orderDetailKey);
        }
    }
}
