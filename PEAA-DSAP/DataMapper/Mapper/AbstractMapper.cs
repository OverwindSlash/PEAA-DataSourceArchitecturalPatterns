using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.CompoundKey;
using DataMapper.Domain;
using DbUtil;

namespace DataMapper.Mapper
{
    public abstract class AbstractMapper
    {
        protected static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;

        protected IDictionary<string, object> parameters = new Dictionary<string, object>();

        protected DomainObject FindSingleById(Key uniqueKey)
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

                IDbCommand command = PrepareCommand(connection, FindSingleStatement());

                PrepareCommandParameters(command);

                IDataReader reader = ExecuteQuery(command);

                reader.Read();

                return (MapRecordToDomainObject((IDataRecord)reader));
            }
        }

        protected abstract DomainObject TryGetFromIdentityMap(Key uniqueKey);

        private static void OpenConnection(IDbConnection connection)
        {
            connection.ConnectionString = DbSettings.ConnectionString;
            connection.Open();
        }

        private IDbCommand PrepareCommand(IDbConnection connection, string statement)
        {
            IDbCommand command = providerFactory.CreateCommand();
            command.Connection = connection;
            command.CommandText = statement;
            return command;
        }

        protected abstract string FindSingleStatement();

        protected abstract void PrepareCommandParameters(IDbCommand command);

        private static IDataReader ExecuteQuery(IDbCommand command)
        {
            IDataReader reader = command.ExecuteReader();
            int AffectedRows = reader.RecordsAffected;
            return reader;
        }

        protected abstract DomainObject MapRecordToDomainObject(IDataRecord record);


        protected IEnumerable<DomainObject> FindMultiByCriteria()
        {
            // You can not search identity map only with where clause.
            // Identity map check will occur in load section.

            // Directly do database query and create corresponded gateway.
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                OpenConnection(connection);

                IDbCommand command = PrepareCommand(connection, FindByCriteriaStatement());

                PrepareCommandParameters(command);

                IDataReader reader = ExecuteQuery(command);

                while (reader.Read())
                {
                    yield return MapRecordToDomainObject((IDataRecord)reader);
                }
            }
        }

        protected abstract string FindByCriteriaStatement();
    }
}
