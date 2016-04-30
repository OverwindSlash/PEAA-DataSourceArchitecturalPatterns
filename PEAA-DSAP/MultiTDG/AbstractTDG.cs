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
                OpenConnection(connection);

                IDbCommand command = PrepareCommand(connection);

                PrepareCommandParameters(command);

                IDataReader reader = ExecuteQuery(command);

                while (reader.Read())
                {
                    yield return ((IDataRecord)reader);
                }
            }
        }

        private static void OpenConnection(IDbConnection connection)
        {
            connection.ConnectionString = DbSettings.ConnectionString;
            connection.Open();
        }

        private IDbCommand PrepareCommand(IDbConnection connection)
        {
            IDbCommand command = providerFactory.CreateCommand();
            command.Connection = connection;
            command.CommandText = GetExecuteReaderSql();
            return command;
        }

        protected abstract string GetExecuteReaderSql();

        protected abstract void PrepareCommandParameters(IDbCommand command);

        private static IDataReader ExecuteQuery(IDbCommand command)
        {
            IDataReader reader = command.ExecuteReader();
            int AffectedRows = reader.RecordsAffected;
            return reader;
        }
    }
}
