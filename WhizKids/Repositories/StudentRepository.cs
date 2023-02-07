using WhizKids.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WhizKids.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IConfiguration _config;

        public StudentRepository(IConfiguration config)
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

        public List<Student> GetAllStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, FirstName, LastName, Enrolled
                                        FROM Student
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Student> students = new List<Student>();
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Enrolled = reader.GetInt32(reader.GetOrdinal("Enrolled"))
                            };

                            students.Add(student);
                        }

                        return students;
                    }
                }
            }
        }

        public Student GetStudentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT s.Id, s.FirstName, s.LastName, s.Enrolled
                                        FROM Student s
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Student student = new Student()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Enrolled = reader.GetInt32(reader.GetOrdinal("Enrolled"))
                            };

                            return student;
                        }

                        return null;
                    }
                }
            }
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Student (FirstName, LastName, Enrolled)
                    OUTPUT INSERTED.ID
                    VALUES (@firstName, @lastName, @enrolled);
                ";

                    cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", student.LastName);
                    cmd.Parameters.AddWithValue("@enrolled", student.Enrolled);

                    int id = (int)cmd.ExecuteScalar();

                    student.Id = id;
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Student
                            SET 
                                FirstName = @firstName,
                                LastName = @lastName,
                                Enrolled = @enrolled, 
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", student.LastName);
                    cmd.Parameters.AddWithValue("@enrolled", student.Enrolled);
                    cmd.Parameters.AddWithValue("@id", student.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Student
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", studentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}