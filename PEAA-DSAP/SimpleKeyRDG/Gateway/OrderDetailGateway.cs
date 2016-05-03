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
    public class OrderDetailGateway : AbstractGateway
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public static OrderDetailGateway Load(IDataRecord record)
        {
            string compoundKey = GenerateCompoundKey(record);

            OrderDetailGateway orderDetailGateway =
                IdentityMap<string, OrderDetailGateway>.Instance.GetEntry(compoundKey);

            if (orderDetailGateway != null)
            {
                return orderDetailGateway;
            }
            
            orderDetailGateway = new OrderDetailGateway();
            orderDetailGateway.OrderID = record.GetInt32("OrderID");
            orderDetailGateway.ProductID = record.GetInt32("ProductID");
            orderDetailGateway.UnitPrice = record.GetDecimal("UnitPrice");
            orderDetailGateway.Quantity = record.GetInt16("Quantity");
            orderDetailGateway.Discount = record.GetFloat("Discount");

            IdentityMap<string, OrderDetailGateway>.Instance.PutEntry(compoundKey, orderDetailGateway);

            return orderDetailGateway;
        }

        private static string GenerateCompoundKey(IDataRecord record)
        {
            return string.Format("{0}:{1}", record.GetInt32("OrderID"), record.GetInt32("ProductID"));
        }
    }
}
