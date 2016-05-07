using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using FullFunctionDataMapper.CompoundKey;
using FullFunctionDataMapper.Domain;
using FullFunctionDataMapper.Statement;

namespace FullFunctionDataMapper.Mapper
{
    public abstract class AbstractMapper
    {
        protected static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;

        //protected IDictionary<string, StatementSource> parameters = new Dictionary<string, StatementSource>();

        protected DomainObject FindSingleById(Key uniqueKey, StatementSource statementSource)
        {
            // Get from identity map.
            DomainObject domainObject = TryGetFromIdentityMap(uniqueKey);
            if (domainObject != null)
            {
                return domainObject;
            }

            // Not found in identity map. Do database query.
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                OpenConnection(connection);

                IDbCommand command = PrepareCommand(connection, statementSource);

                PrepareCommandParameters(command, statementSource);

                IDataReader reader = ExecuteQuerySingle(command);

                return (MapRecordToDomainObject((IDataRecord)reader));
            }
        }

        protected abstract DomainObject TryGetFromIdentityMap(Key uniqueKey);

        private static void OpenConnection(IDbConnection connection)
        {
            connection.ConnectionString = DbSettings.ConnectionString;
            connection.Open();
        }

        private IDbCommand PrepareCommand(IDbConnection connection, StatementSource statementSource)
        {
            IDbCommand command = providerFactory.CreateCommand();
            command.Connection = connection;
            command.CommandText = statementSource.Statement;
            return command;
        }

        private void PrepareCommandParameters(IDbCommand command, StatementSource statementSource)
        {
            foreach (IDbDataParameter dbDataParameter in statementSource.Parameters)
            {
                DbParameter parameter = providerFactory.CreateParameter();
                parameter.ParameterName = dbDataParameter.ParameterName;
                parameter.DbType = dbDataParameter.DbType;
                parameter.Value = dbDataParameter.Value;
                command.Parameters.Add(parameter);
            }
        }

        private static IDataReader ExecuteQuerySingle(IDbCommand command)
        {
            IDataReader reader = command.ExecuteReader();
            reader.Read();

            return reader;
        }

        protected abstract DomainObject MapRecordToDomainObject(IDataRecord record);
    }
}
