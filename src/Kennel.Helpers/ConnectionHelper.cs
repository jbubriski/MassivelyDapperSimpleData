using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Helpers
{
    public class ConnectionHelper
    {
        public static IDbConnection GetConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

            connection.Open();

            return connection;
        }
    }
}
