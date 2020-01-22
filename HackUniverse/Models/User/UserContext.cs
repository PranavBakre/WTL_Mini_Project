using HackUniverse.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public dynamic GetUserByUserName(string username)
        {
            User user=null;
            Profile profile=null;
            using (MySqlConnection connection = GetConnecton())
            {
                
                connection.Open();
                var cmd = new MySqlCommand($"select * from user where username = '{username}'",connection);
                //cmd.Parameters.AddWithValue("@username",username);
                using(var read = cmd.ExecuteReader())
                {
                    while(read.Read())
                    {
                        user= new User
                        {
                            UserName = read["username"].ToString(),
                            Email = read["email"].ToString(),
                            Password = read["password"].ToString()
                        };
                    }
                }
                if (user != null) { 
                var profileData = new MySqlCommand($"select * from profile where username= '{username}' ", connection);
                //profileData.Parameters.AddWithValue("@username",username);
                using (var read = profileData.ExecuteReader())
                {
                    while (read.Read())
                    {
                            profile = new Profile
                            {
                                Username = read["username"].ToString(),
                                FirstName = read["FirstName"].ToString(),
                                LastName = read["LastName"].ToString(),
                                Occupation = read["Occupation"].ToString(),
                                Organization = read["OrganizationName"].ToString(),
                                ProfilePicture = utilities.ObjecttoByteArray(read["ProfilePicture"]),
                                ContactPhone = read["ContactPhone"].ToString(),
                                Type = Convert.ToChar(read["Type"])

                            };
                    }
                }
                }
            }
            if (user != null) { 
                dynamic Model = new ExpandoObject();
                Model.User = user;
                Model.Profile = profile;

                return Model;
            }

            return null;
        }

        public bool LoginRequest(string username,string password)
        {
            using ( MySqlConnection conn = GetConnecton()) {
                conn.Open();
                var cmd = new MySqlCommand($"select * from user where username='{username}' and password=_binary '{password}'",conn);
                using (var read = cmd.ExecuteReader())
                {
                    return read.Read();
                }
                
            }

        }

        public bool CheckUserNameTaken(string username)
        {
            using (MySqlConnection conn=GetConnecton())
            {
                conn.Open();
                var cmd = new MySqlCommand($"select * from user where username='{username}'",conn);
                return cmd.ExecuteReader().Read();
                
            }
        }

        public bool RegisterUser(string username, string password, string email, string FirstName, string LastName, string Occupation,
            string OrganizationName,string ContactPhone, object ProfilePicture, char UserType)
        {
            string query = $"insert into user(username,email,password) values('{username}','{email}','{password}')";
            using( var connection = GetConnecton())
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                if (command.ExecuteNonQuery()>0)
                {
                    query = $"insert into profile(username,FirstName,LastName,Occupation,OrganizationName,ProfilePicture,ContactPhone,Type) values('{username}','{FirstName}','{LastName}','{Occupation}','{OrganizationName}','{ProfilePicture}','{ContactPhone}','{UserType}')";
                    command = new MySqlCommand(query, connection);
                    return command.ExecuteNonQuery()>0?true:false;
                }


            }
            return false;
        }

    }
}
