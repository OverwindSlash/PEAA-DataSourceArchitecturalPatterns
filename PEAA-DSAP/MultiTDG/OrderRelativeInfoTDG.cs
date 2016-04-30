using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using DTO;

namespace MultiTDG
{
    public class OrderRelativeInfoTDG : AbstractTDG
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>();

        protected override string GetExecuteReaderSql()
        {
            return "SELECT [Order Details].OrderID, [Products].ProductName, [Order Details].Quantity, [Categories].CategoryID, [Categories].CategoryName, [Order Details].Discount " +
            "FROM [Order Details], [Products], [Categories] " +
            "WHERE [Order Details].ProductID = [Products].ProductID AND [Products].CategoryID = [Categories].CategoryID " +
            "AND [Order Details].OrderID = @OrderID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@OrderID";
            parameter.DbType = DbType.Int32;
            parameter.Value = parameters[parameter.ParameterName];
            command.Parameters.Add(parameter);
        }

        public IEnumerable<OrderRelativeInfoDto> FindOrderRelativeInfoByOrderId(int orderId)
        {
            parameters["@OrderID"] = orderId;

            IList<OrderRelativeInfoDto> orderRelativeInfoDtos = new List<OrderRelativeInfoDto>();

            foreach (IDataRecord dataRecord in ExecuteReaderById(orderId))
            {
                orderRelativeInfoDtos.Add(CreateOrderRelativeInfoDto(dataRecord));
            }

            return orderRelativeInfoDtos;
        }

        private OrderRelativeInfoDto CreateOrderRelativeInfoDto(IDataRecord dataRecord)
        {
            OrderRelativeInfoDto orderRelativeInfoDto = new OrderRelativeInfoDto();

            orderRelativeInfoDto.OrderID = dataRecord.GetInt32("OrderID");
            orderRelativeInfoDto.ProductName = dataRecord.GetString("ProductName");
            orderRelativeInfoDto.CategoryID = dataRecord.GetInt32("CategoryID");
            orderRelativeInfoDto.CategoryName = dataRecord.GetString("CategoryName");
            orderRelativeInfoDto.Quantity = dataRecord.GetInt16("Quantity");
            orderRelativeInfoDto.Discount = dataRecord.GetFloat("Discount");

            return orderRelativeInfoDto;
        }
    }
}
