using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Model
{
    public static class GlobalConfig
    {
        public static string SetTable(string query, string table)
        {
            return query.Replace("Table", table);
        }

        public static DateTime GetMexDate()
        {
            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central Standard Time (Mexico)");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
