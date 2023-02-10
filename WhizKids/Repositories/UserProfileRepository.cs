using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using WhizKids.Models;

namespace WhizKids.Repositories
{
        public class UserProfileRepository : IUserProfileRepository
        {
            private readonly IConfiguration _config;

            public UserProfileRepository(IConfiguration config)
            {
                _config = config;
            }

            public SqlConnection Connection
            {
                get
                {
                    return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                }
            }

        public List<UserProfile> GetAllUsersProfiles()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, FirebaseUserId, FirstName, LastName, PhoneNumber, Address, IsAdmin
                                        FROM UserProfile
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            UserProfile user = new UserProfile
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")),
                            };

                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
        }

        public UserProfile GetUserProfileById(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                           SELECT Id, FirebaseUserId, FirstName, LastName, Email, Address, PhoneNumber, IsAdmin, StudentId
                           FROM UserProfile 
                           
                           WHERE Id = @id";

                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            UserProfile userProfile = null;
                            while (reader.Read())
                            {
                                if (userProfile == null)
                                {
                                    userProfile = new UserProfile
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        Address = reader.GetString(reader.GetOrdinal("Address")),
                                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                        IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")),
                                        //StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),

                                    };
                                }
                            }
                            return userProfile;
                        }
                    }
                }
            }

            public void AddUserProfile(UserProfile user)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO UserProfile (FirebaseUserId, FirstName, LastName, PhoneNumber, Address, IsAdmin)
                            OUTPUT INSERTED.ID 
                            VALUES (@firebaseuserid, @firstname, @lastname, @phonenumber, @address, @isadmin)";

                        cmd.Parameters.AddWithValue("@firebaseuserid", user.FirebaseUserId);
                        cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", user.LastName);
                        cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@address", user.Address);
                        cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);

                        int id = (int)cmd.ExecuteScalar();

                        user.Id = id;
                    }
                }
            }

            public void UpdateUserProfile(UserProfile user)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            UPDATE UserProfile
                            SET 
                                FirebaseUserId = @firebaseuserid
                                FirstName = @firstname,
                                LastName = @lastname
                                Address = @address, 
                                PhoneNumber = @phonenumber,
                                IsAdmin = @isadmin,
                                StudentId = @studentid
                            WHERE Id = @id";

                        cmd.Parameters.AddWithValue("@firebaseuserid", user.FirebaseUserId);
                        cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", user.LastName);
                        cmd.Parameters.AddWithValue("@address", user.Address);
                        cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
                    cmd.Parameters.AddWithValue("@studentid", user.StudentId);
                        cmd.Parameters.AddWithValue("@id", user.Id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public void DeleteUserProfile(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DELETE FROM UserProfile
                            WHERE Id = @id
                        ";

                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

        public List<UserProfile> GetUserProfilesById(int userProfileId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, FirebaseUserId, FirstName, LastName, Email, Address, PhoneNumber, IsAdmin, StudentId
                           FROM UserProfile 
                           
                           WHERE Id = @id";
                    

                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<UserProfile> users = new List<UserProfile>();

                        while (reader.Read())
                        {
                            UserProfile user = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")),
                                //StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            };                           

                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
        }

        public List<UserProfile> GetAllUserStudentsProfiles()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT u.Id, u.FirebaseUserId, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Address, u.IsAdmin
                                        FROM UserProfile u
                                        LEFT JOIN UserStudent us ON us.UserProfileId = u.Id
                                        LEFT JOIN Student s ON us.StudentId = s.Id
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            UserProfile user = new UserProfile
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")),
                            };

                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
        }
    }
    }

