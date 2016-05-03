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
    public class CategoryFinder : AbstractFinder
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>();

        protected override AbstractGateway TryGetFromIdentityMap(int uniqueId)
        {
            return IdentityMap<int, CategoryGateway>.Instance.GetEntry(uniqueId);
        }

        protected override string GetExecuteReaderSql()
        {
            return "SELECT * FROM [Categories] " +
            "WHERE [Categories].CategoryID = @CategoryID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@CategoryID";
            parameter.DbType = DbType.Int32;
            parameter.Value = parameters[parameter.ParameterName];
            command.Parameters.Add(parameter);
        }

        protected override AbstractGateway CreateGateway(IDataRecord reader)
        {
            return CategoryGateway.Load(reader);
        }

        public CategoryGateway FindCategoryGatewayById(int categoryId)
        {
            parameters["@CategoryID"] = categoryId;

            return (CategoryGateway)FindSingleById(categoryId);
        }
    }
}
