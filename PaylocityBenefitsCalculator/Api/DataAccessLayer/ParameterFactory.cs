namespace Api.DataAccessLayer
{
    using System;
    using System.Data;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Although there is no database and hence these methods aren't needed, I thought
    /// it would be a good idea to demonstrate how the arguments to the calls to the stored procedures
    /// would be constructed.
    /// </summary>
    /// <remarks>
    /// I had to add the appropriate nuget packages in order to use SqlParameter.
    /// </remarks>
    public static class ParameterFactory
    {
        public static IDataParameter CreateVarChar(string name, string value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static IDataParameter CreateInt(string name, int value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Int;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static IDataParameter CreateTinyInt(string name, byte value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.TinyInt;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static IDataParameter CreateDate(string name, DateTime value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Date;
            parameter.ParameterName = name;
            parameter.Value = value.Date;
            return parameter;
        }

        public static IDataParameter CreateMoney(string name, decimal value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Decimal;
            parameter.Precision = 11;
            parameter.Scale = 2;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}
