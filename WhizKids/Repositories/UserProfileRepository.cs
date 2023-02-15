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
                                    SELECT Id, FirebaseUserId, FirstName, LastName, Email, Address, PhoneNumber, IsAdmin
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
                                Email = reader.GetString(reader.GetOrdinal("Email"))
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
                           SELECT Id, FirebaseUserId, FirstName, LastName, Email, Address, PhoneNumber, IsAdmin
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
                                        

                                    };
                                }
                            }
                            return userProfile;
                        }
                    }
                }
            }

            public void AddUserProfile(UserProfile user, UserStudent userStudent)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO UserProfile  (FirebaseUserId, FirstName, LastName, Email, Address, PhoneNumber, IsAdmin)
                            OUTPUT INSERTED.ID 
                            VALUES (@firebaseuserid, @firstname, @lastname, @email, @address, @phonenumber, @isadmin)

                            INSERT INTO UserStudent  (StudentId)
                            OUTPUT INSERTED.ID USERPROFILEID
                            VALUES (@studentid)
                            ";

                        cmd.Parameters.AddWithValue("@firebaseuserid", user.FirebaseUserId);
                        cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", user.LastName);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@address", user.Address);
                        cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
                        cmd.Parameters.AddWithValue("@studentid", userStudent.StudentId);



                    int userId = (int)cmd.ExecuteScalar();                    
                    int userStudentId = (int)cmd.ExecuteScalar();
                    int userProfileId = userId;

                    userStudent.UserProfileId = userProfileId;
                    user.Id = userId;
                    userStudent.Id = userStudentId; 
                    }
                }
            }
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                           SELECT Id, FirebaseUserId, FirstName, LastName, Email, Address, PhoneNumber, IsAdmin
                           FROM UserProfile  
                           
                           WHERE FirebaseUserId = @firebaseUserid";

                    cmd.Parameters.AddWithValue("@firebaseUserId", firebaseUserId);

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
                                    

                                };
                            }
                        }
                        return userProfile;
                    }
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
                                FirebaseUserId = @firebaseuserid,
                                FirstName = @firstname,
                                LastName = @lastname,
                                Address = @address, 
                                PhoneNumber = @phonenumber,
                                IsAdmin = @isadmin,
                                WHERE Id = @id";

                        cmd.Parameters.AddWithValue("@firebaseuserid", user.FirebaseUserId);
                        cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", user.LastName);
                        cmd.Parameters.AddWithValue("@address", user.Address);
                        cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
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

        public List<UserProfile> GetUserProfilesByStudentId(int studentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT up.Id AS UserProfileId, up.FirebaseUserId, up.FirstName, up.LastName, up.Email, up.Address, up.PhoneNumber, up.IsAdmin
                           FROM UserProfile up 
                           JOIN UserStudent us ON up.Id = us.UserProfileId
                           JOIN Student s ON us.StudentId = s.Id
                           WHERE s.Id = @studentId";
                    

                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<UserProfile> userProfiles = new List<UserProfile>();

                        while (reader.Read())
                        {
                            UserProfile userProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")),
                                
                            };                           

                            userProfiles.Add(userProfile);
                        }

                        return userProfiles;
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
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
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

