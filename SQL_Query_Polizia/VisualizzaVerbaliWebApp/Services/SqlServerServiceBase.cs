﻿using System.Data.Common;
using System.Data.SqlClient;

// Inserisco le proprietà e metodi di SqlServerServiceBase in VerbaleService
// SERVIZIO INUTILIZZATO
namespace VisualizzaVerbaliWebApp.Services
{
    public class SqlServerServiceBase
    {
        private SqlConnection _connection;

        public SqlServerServiceBase(IConfiguration config)
        {
            _connection = new SqlConnection(config.GetConnectionString("Verbali"));
        }
        protected DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _connection);
        }

        protected DbConnection GetConnection()
        {
            return _connection;
        }
    }
}
