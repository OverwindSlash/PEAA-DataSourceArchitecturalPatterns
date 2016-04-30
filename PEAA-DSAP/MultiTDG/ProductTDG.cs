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
    public class ProductTDG : AbstractTDG
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>(); 

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

        public IEnumerable<ProductDto> FindProductById(int productId)
        {
            parameters["@ProductID"] = productId;

            IList<ProductDto> productDtos = new List<ProductDto>();

            foreach (IDataRecord dataRecord in ExecuteReaderById(productId))
            {
                productDtos.Add(CreateProductDto(dataRecord));
            }

            return productDtos;
        }

        private ProductDto CreateProductDto(IDataRecord dataRecord)
        {
            ProductDto productDto = new ProductDto();

            productDto.ProductID = dataRecord.GetInt32("ProductID");
            productDto.ProductName = dataRecord.GetString("ProductName");
            productDto.CategoryID = dataRecord.GetInt32("CategoryID");

            return productDto;
        }
    }
}
