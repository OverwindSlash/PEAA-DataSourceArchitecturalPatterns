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
    public class CategoryTDG : AbstractTDG
    {
        private IDictionary<string, object> parameters = new Dictionary<string, object>();

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

        public IEnumerable<CategoryDto> FindCategoryById(int categoryId)
        {
            parameters["@CategoryID"] = categoryId;

            IList<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (IDataRecord dataRecord in ExecuteReaderById(categoryId))
            {
                categoryDtos.Add(CreateCategoryDto(dataRecord));
            }

            return categoryDtos;
        }

        private CategoryDto CreateCategoryDto(IDataRecord dataRecord)
        {
            CategoryDto categoryDto = new CategoryDto();

            categoryDto.CategoryID = dataRecord.GetInt32("CategoryID");
            categoryDto.CategoryName = dataRecord.GetString("CategoryName");

            return categoryDto;
        }
    }
}
