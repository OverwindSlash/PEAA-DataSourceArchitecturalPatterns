using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using SimpleKeyRDG.Gateway;

namespace SimpleKeyRDG.Finder
{
    public abstract class AbstractFinder
    {
        protected static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;

        protected AbstractGateway FindSingleById(int uniqueId)
        {
            // Get from identity map.
            AbstractGateway abstractGateway = TryGetFromIdentityMap(uniqueId);
            if (abstractGateway != null)
            {
                return abstractGateway;
            }

            // Not found in identity map. Do database query.
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                OpenConnection(connection);

                IDbCommand command = PrepareCommand(connection);

                PrepareCommandParameters(command);

                IDataReader reader = ExecuteQuery(command);

                reader.Read();

                return (CreateGateway((IDataRecord)reader));
            }
        }

        protected abstract AbstractGateway TryGetFromIdentityMap(int uniqueId);

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

        protected abstract AbstractGateway CreateGateway(IDataRecord reader);



        protected IEnumerable<AbstractGateway> FindMultiByCriteria()
        {
            // You can not search identity map only with where clause.
            // Identity map check will occur in load section.

            // Directly do database query and create corresponded gateway.
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                OpenConnection(connection);

                IDbCommand command = PrepareCommand(connection);

                PrepareCommandParameters(command);

                IDataReader reader = ExecuteQuery(command);

                while (reader.Read())
                {
                    yield return CreateGateway((IDataRecord)reader);
                }
            }
        }
    }
}
