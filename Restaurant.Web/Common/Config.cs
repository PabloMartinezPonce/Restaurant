using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.Common
{
    public static class Config
    {
        public static string SetTable(string query, string table)
        {
            return query.Replace("Table", table);
        }
    }
}
