using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Mapper
{
    public class StatementSource
    {
        public string Statement { get; set; }
        public IList<IDbDataParameter> Parameters { get; set; }
    }
}
