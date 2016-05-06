using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;

namespace FullFunctionDataMapper.Mapper
{
    public abstract class AbstractMapper
    {
        protected static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;
    }
}
