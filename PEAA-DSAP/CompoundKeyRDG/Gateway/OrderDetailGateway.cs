using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompoundKeyRDG.CompoundKey;
using CompoundKeyRDG.IdentityMap;
using DbUtil;

namespace CompoundKeyRDG.Gateway
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
            Key orderDetailKey = new Key(record.GetInt32("OrderID"), record.GetInt32("ProductID"));

            OrderDetailGateway orderDetailGateway =
                IdentityMap<Key, OrderDetailGateway>.Instance.GetEntry(orderDetailKey);

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

            IdentityMap<Key, OrderDetailGateway>.Instance.PutEntry(orderDetailKey, orderDetailGateway);

            return orderDetailGateway;
        }
    }
}
