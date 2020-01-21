using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackUniverse.Models.User
{
    public class UserContext
    {
            public String ConnectionString { get; set; }
            public UserContext(string connectionString)
            {
                ConnectionString = connectionString;
            }

            private MySqlConnection GetConnecton()
            {
                return new MySqlConnection(ConnectionString);
            }
        }
}
