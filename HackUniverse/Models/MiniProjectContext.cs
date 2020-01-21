using HackUniverse.Utilities;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HackUniverse.Models
{
#nullable enable
    public class MiniProjectContext
    {
        public String ConnectionString { get; set; }
        public MiniProjectContext ( string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnecton()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Hackathon> GetAllHackathons()
        {
            var list = new List<Hackathon>();
            using (MySqlConnection conn = GetConnecton()) {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Hackathon", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Hackathon()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Subtitle = reader["Subtitle"].ToString(),
                            Description = reader["Description"].ToString(),
                            ContactMail = reader["ContactMail"].ToString(),
                            ContactPhone = reader["ContactPhone"].ToString(),
                            ContactWebsite = reader["ContactWebsite"].ToString(),
                            CoverPhoto = utilities.ObjecttoByteArray(reader["CoverPhoto"]),
                            Thumbnail = utilities.ObjecttoByteArray(reader["Thumbnail"]),
                            StartDate = (System.DateTime)reader["StartDate"],
                            EndDate = (System.DateTime)reader["EndDate"]


                        }); ;
                    }
                }
            }
            return list;
        }

    }

    
}
