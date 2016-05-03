using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUtil
{
    public static class DataRecordExtensions
    {
        // Database null value safe getter.
        public static int? GetInt32OrNull(this IDataRecord record, int ordinal)
        {
            return record.IsDBNull(ordinal) ? null : (int?)record.GetInt32(ordinal);
        }

        public static int? GetInt32OrNull(this IDataRecord record, string columnName)
        {
            return record.GetInt32OrNull(record.GetOrdinal(columnName));
        }

        public static short? GetInt16OrNull(this IDataRecord record, int ordinal)
        {
            return record.IsDBNull(ordinal) ? null : (short?)record.GetInt16(ordinal);
        }

        public static short? GetInt16OrNull(this IDataRecord record, string columnName)
        {
            return record.GetInt16OrNull(record.GetOrdinal(columnName));
        }

        public static decimal? GetDecimalOrNull(this IDataRecord record, int ordinal)
        {
            return record.IsDBNull(ordinal) ? null : (decimal?)record.GetDecimal(ordinal);
        }

        public static decimal? GetDecimalOrNull(this IDataRecord record, string columnName)
        {
            return record.GetDecimalOrNull(record.GetOrdinal(columnName));
        }

        //public static string GetString(this IDataRecord record, int ordinal)
        //{
        //    return record.IsDBNull(ordinal) ? string.Empty : record.GetString(ordinal);
        //}

        // Database value helper getter.
        public static string GetString(this IDataRecord record, string columnName)
        {
            return record.GetString(record.GetOrdinal(columnName));
        }

        public static int GetInt32(this IDataRecord record, string columnName)
        {
            return record.GetInt32(record.GetOrdinal(columnName));
        }

        public static short GetInt16(this IDataRecord record, string columnName)
        {
            return record.GetInt16(record.GetOrdinal(columnName));
        }

        public static decimal GetDecimal(this IDataRecord record, string columnName)
        {
            return record.GetDecimal(record.GetOrdinal(columnName));
        }

        public static float GetFloat(this IDataRecord record, string columnName)
        {
            return record.GetFloat(record.GetOrdinal(columnName));
        }

        public static bool GetBoolean(this IDataRecord record, string columnName)
        {
            return record.GetBoolean(record.GetOrdinal(columnName));
        }
    }
}
