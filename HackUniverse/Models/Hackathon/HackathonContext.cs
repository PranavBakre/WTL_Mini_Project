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

        public List<Hackathon> Search(string S)
        {
            List<Hackathon> list=new List<Hackathon>();
            using (MySqlConnection connection = GetConnecton())
            {
                connection.Open();
                var cmd = new MySqlCommand($"select * from Hackathon  where match(title, subtitle) against('{S}');", connection);
                using (var read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                       list.Add(new Hackathon
                        {
                            Id = Convert.ToInt32(read["Id"].ToString()),
                            Title = read["Title"].ToString(),
                            Subtitle = read["Subtitle"].ToString(),
                            Description = read["Description"].ToString(),
                            ContactMail = read["ContactMail"].ToString(),
                            ContactPhone = read["ContactPhone"].ToString(),
                            ContactWebsite = read["ContactWebsite"].ToString(),
                            CoverPhoto = utilities.ObjecttoByteArray(read["CoverPhoto"]),
                            Thumbnail = //read["Thumbnail"].ToString(),
                            utilities.ObjecttoByteArray(read["Thumbnail"]),
                            //read.GetString("Thumbnail"),
                            StartDate = (System.DateTime)read["StartDate"],
                            EndDate = (System.DateTime)read["EndDate"]
                        });
                    }
                }
            }
            return list;
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
                        Id = Convert.ToInt32(read["Id"].ToString()),
                        Title = read["Title"].ToString(),
                        Subtitle = read["Subtitle"].ToString(),
                        Description = read["Description"].ToString(),
                        ContactMail = read["ContactMail"].ToString(),
                        ContactPhone = read["ContactPhone"].ToString(),
                        ContactWebsite = read["ContactWebsite"].ToString(),
                        CoverPhoto = utilities.ObjecttoByteArray(read["CoverPhoto"]),
                        Thumbnail = //read["Thumbnail"].ToString(),
                        utilities.ObjecttoByteArray(read["Thumbnail"]),
                        //read.GetString("Thumbnail"),
                        StartDate = (System.DateTime)read["StartDate"],
                        EndDate = (System.DateTime)read["EndDate"]
                    };
                }
            }
        }

        public Hackathon GetByName(string title)
        {
            using (MySqlConnection connection = GetConnecton())
            {
                connection.Open();
                var cmd = new MySqlCommand($"select * from Hackathon where Title='{title}'", connection);
                using (var read = cmd.ExecuteReader())
                {
                    read.Read();
                    return new Hackathon
                    {
                        Id = Convert.ToInt32(read["Id"].ToString()),
                        Title = read["Title"].ToString(),
                        Subtitle = read["Subtitle"].ToString(),
                        Description = read["Description"].ToString(),
                        ContactMail = read["ContactMail"].ToString(),
                        ContactPhone = read["ContactPhone"].ToString(),
                        ContactWebsite = read["ContactWebsite"].ToString(),
                        CoverPhoto = utilities.ObjecttoByteArray(read["CoverPhoto"]),
                        Thumbnail = //read["Thumbnail"].ToString(),
                        utilities.ObjecttoByteArray(read["Thumbnail"]),
                        //read.GetString("Thumbnail"),
                        StartDate = (System.DateTime)read["StartDate"],
                        EndDate = (System.DateTime)read["EndDate"]
                    };
                }
            }
        }


        /*        public List<Hackathon> GetAllHackathons()
                {
                    var list = new List<Hackathon>();
                    using (MySqlConnection conn = GetConnecton()) {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("select * from Hackathon", conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hackathon h;

                                h = new Hackathon()
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


                                };
                                //try
                                //{
                                //    h.Thumbnail = //utilities.ObjecttoByteArray(reader["Thumbnail"]),
                                //    //reader["Thumbnail"].ToString(),
                                //    reader.GetString("Thumbnail");

                                //}
                                //catch (Exception ex)
                                //{
                                //    h.Thumbnail = "";
                                //}
                                list.Add(h);
                            }
                            conn.Close();
                        }
                    }
                    return list;
                }

            }


        }
        */
        public List<Hackathon> GetPreviousHackathons()
        {
            var list = new List<Hackathon>();
            using (MySqlConnection conn = GetConnecton()) {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Hackathon where date(enddate)<current_date order by enddate desc limit 12", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hackathon h;

                        h = new Hackathon()
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


                        };
                        //try
                        //{
                        //    h.Thumbnail = //utilities.ObjecttoByteArray(reader["Thumbnail"]),
                        //    //reader["Thumbnail"].ToString(),
                        //    reader.GetString("Thumbnail");
                            
                        //}
                        //catch (Exception ex)
                        //{
                        //    h.Thumbnail = "";
                        //}
                        list.Add(h);
                    }
                    conn.Close();
                }
            }
            return list;
        }

        public List<Hackathon> GetNextHackathons()
        {
            var list = new List<Hackathon>();
            using (MySqlConnection conn = GetConnecton()) {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Hackathon where date(startdate)>current_date order by startdate asc limit 12", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hackathon h;

                        h = new Hackathon()
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


                        };
                        //try
                        //{
                        //    h.Thumbnail = //utilities.ObjecttoByteArray(reader["Thumbnail"]),
                        //    //reader["Thumbnail"].ToString(),
                        //    reader.GetString("Thumbnail");
                            
                        //}
                        //catch (Exception ex)
                        //{
                        //    h.Thumbnail = "";
                        //}
                        list.Add(h);
                    }
                    conn.Close();
                }
            }
            return list;
        }

        public List<Hackathon> GetCurrentHackathons()
        {
            var list = new List<Hackathon>();
            using (MySqlConnection conn = GetConnecton()) {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Hackathon where date(startdate)<=current_date and date(enddate)>=current_date order by startdate asc limit 12", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hackathon h;

                        h = new Hackathon()
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


                        };
                        //try
                        //{
                        //    h.Thumbnail = //utilities.ObjecttoByteArray(reader["Thumbnail"]),
                        //    //reader["Thumbnail"].ToString(),
                        //    reader.GetString("Thumbnail");
                            
                        //}
                        //catch (Exception ex)
                        //{
                        //    h.Thumbnail = "";
                        //}
                        list.Add(h);
                    }
                    conn.Close();
                }
            }
            return list;
        }

    }

    
}
