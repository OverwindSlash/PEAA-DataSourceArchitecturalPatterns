using DbUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTDG
{
    public abstract class AbstractTDG
    {
        protected static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;

        protected IEnumerable<IDataRecord> ExecuteReaderById(int uniqueId)
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = GetExecuteReaderSql();

                PrepareCommandParameters(command);

                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return ((IDataRecord)reader);
                }
            }
        }

        protected abstract string GetExecuteReaderSql();
        protected abstract void PrepareCommandParameters(IDbCommand command);
    }
}
