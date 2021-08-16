using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Web.Common
{
    public class SqlConfiguration
    {
        public SqlConfiguration(string _connectionString) => connectionString = _connectionString;

        public string connectionString { get; }

    }
}
