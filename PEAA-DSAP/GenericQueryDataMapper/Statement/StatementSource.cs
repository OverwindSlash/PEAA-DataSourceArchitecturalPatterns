using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericQueryDataMapper.Statement
{
    public class StatementSource
    {
        public string Statement { get; set; }
        public IList<IDbDataParameter> Parameters { get; set; }

        public StatementSource()
        {
            Parameters = new List<IDbDataParameter>();
        }
    }
}
