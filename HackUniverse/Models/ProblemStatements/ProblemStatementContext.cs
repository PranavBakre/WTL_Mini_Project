using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackUniverse.Models.ProblemStatements
{
    public class ProblemStatementContext
    {

        public String ConnectionString { get; set; }
        public ProblemStatementContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnecton()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<ProblemStatement> GetProblemStatementsById(int Hid)
        {
            var list = new List<ProblemStatement>();
            using (MySqlConnection connection = GetConnecton())
            {

                connection.Open();
                var cmd = new MySqlCommand($"select * from problemstatement where HackathonId='{Hid}'", connection);
                using (var read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        list.Add(new ProblemStatement
                        {
                            Id = Convert.ToInt32(read["Id"]),
                            HackathonId = Convert.ToInt32(read["HackathonId"]),
                            problemStatement = read["ProblemStatement"].ToString(),
                            Description = read["Description"].ToString(),
                            Category = read["Category"].ToString()

                        });
                    }
                    connection.Close();
                }
            }
            return list;
        }
    }
}
