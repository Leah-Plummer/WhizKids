using WhizKids.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WhizKids.Repositories
{
    public class UserStudentRepository : IUserStudentRepository
    {
        private readonly IConfiguration _config;

        public UserStudentRepository(IConfiguration config)
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

        public List<UserStudent> GetAllUserStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, UserProfileId, StudentId
                                        FROM UserStudent
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserStudent> userStudents = new List<UserStudent>();
                        while (reader.Read())
                        {
                            UserStudent userStudent = new UserStudent
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            };

                            userStudents.Add(userStudent);
                        }

                        return userStudents;
                    }
                }
            }
        }

        public UserStudent GetUserStudentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT us.Id, us.UserProfileId, us.StudentId
                                        FROM UserStudent us
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserStudent userStudent = new UserStudent()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"))
                            };

                            return userStudent;
                        }

                        return null;
                    }
                }
            }
        }





        public void AddUserStudent(int studentId, int userProfileId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserStudent (UserProfileId, StudentId)
                    OUTPUT INSERTED.ID
                    VALUES (@userProfileId, @studentId);
                ";

                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    int id = (int)cmd.ExecuteScalar();


                    
                }
            }
        }

        public void UpdateUserStudent(int studentId, int userProfileId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE UserStudent
                            SET 
                                UserProfileId = @userProfileId,
                                StudentId = @studenteId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserStudent(int userStudentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM UserStudent
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", userStudentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}