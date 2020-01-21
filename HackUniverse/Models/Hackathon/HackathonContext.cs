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
    public class HackathonContext
    {
        public String ConnectionString { get; set; }
        public HackathonContext ( string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnecton()
        {
            return new MySqlConnection(ConnectionString);
        }

        public Hackathon GetByID(int id)
        {
            using (MySqlConnection connection = GetConnecton())
            {
                connection.Open();
                var cmd = new MySqlCommand($"select * from Hackathon where Id='{id}'",connection);
                using (var read = cmd.ExecuteReader())
                {
                    read.Read();
                    return new Hackathon
                    {
                        Title = read["Title"].ToString(),
                        Subtitle = read["Subtitle"].ToString(),
                        Description = read["Description"].ToString(),
                        ContactMail = read["ContactMail"].ToString(),
                        ContactPhone = read["ContactPhone"].ToString(),
                        ContactWebsite = read["ContactWebsite"].ToString(),
                        CoverPhoto = utilities.ObjecttoByteArray(read["CoverPhoto"]),
                        Thumbnail = utilities.ObjecttoByteArray(read["Thumbnail"]),
                        StartDate = (System.DateTime)read["StartDate"],
                        EndDate = (System.DateTime)read["EndDate"]
                    };
                }
                connection.Close();
            }
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
                    conn.Close();
                }
            }
            return list;
        }

    }

    
}
