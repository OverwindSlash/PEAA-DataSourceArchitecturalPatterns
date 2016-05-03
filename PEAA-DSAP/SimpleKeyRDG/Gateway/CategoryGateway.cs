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
    public class CategoryGateway : AbstractGateway
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public static CategoryGateway Load(IDataRecord reader)
        {
            int categoryId = reader.GetInt32("CategoryID");

            CategoryGateway categoryGateway = IdentityMap<int, CategoryGateway>.Instance.GetEntry(categoryId);
            if (categoryGateway != null)
            {
                return categoryGateway;
            }

            categoryGateway = new CategoryGateway();

            categoryGateway.CategoryID = reader.GetInt32("CategoryID");
            categoryGateway.CategoryName = reader.GetString("CategoryName");
            categoryGateway.Description = reader.GetString("Description");
            categoryGateway.Picture = (byte[])reader.GetValue(reader.GetOrdinal("Picture"));

            IdentityMap<int, CategoryGateway>.Instance.PutEntry(categoryGateway.CategoryID, categoryGateway);

            return categoryGateway;
        }
    }
}
