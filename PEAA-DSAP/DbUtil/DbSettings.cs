using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUtil
{
    public static class DbSettings
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Default"].ToString();
            }
        }

        public static DbProviderFactory ProviderFactory
        {
            get
            {
                string providerName = ConfigurationManager.AppSettings["Provider"].ToString();
                return DbProviderFactories.GetFactory(providerName);
            }
        }
    }
}
