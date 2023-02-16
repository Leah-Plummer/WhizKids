using WhizKids.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WhizKids.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IConfiguration _config;

        public MessageRepository(IConfiguration config)
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

        public List<Message> GetAllMessages()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, StudentId, UserProfileId, CreateDateTime, Body
                                        FROM Messages
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Message> messages = new List<Message>();
                        while (reader.Read())
                        {
                            Message message = new Message
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                Body = reader.GetString(reader.GetOrdinal("Body")),
                            };

                            messages.Add(message);
                        }

                        return messages;
                    }
                }
            }
        }

        public List<Message> GetAllMessagesById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, StudentId, UserProfileId, CreateDateTime, Body
                                        FROM Messages
                                        Where StudentId = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Message> messages = new List<Message>();
                        while (reader.Read())
                        {
                            Message message = new Message
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                Body = reader.GetString(reader.GetOrdinal("Body")),
                            };

                            messages.Add(message);
                        }

                        return messages;
                    }
                }
            }
        }

        public Message GetMessageById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT m.Id, m.StudentId, m.UserProfileId, m.CreateDateTime, m.Body
                                        FROM Messages m
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Message message = new Message()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                Body = reader.GetString(reader.GetOrdinal("Body"))
                            };

                            return message;
                        }

                        return null;
                    }
                }
            }
        }





        public void AddMessage(Message message)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Messages (StudentId, UserProfileId, CreateDateTime, Body)
                    OUTPUT INSERTED.ID
                    VALUES (@studentId, @userProfileId, @createTime, @body);
                ";

                    cmd.Parameters.AddWithValue("@studentId", message.StudentId);
                    cmd.Parameters.AddWithValue("@userProfileId", message.UserProfileId);
                    cmd.Parameters.AddWithValue("@createDateTime", message.CreateDateTime);
                    cmd.Parameters.AddWithValue("@body", message.Body);

                    int id = (int)cmd.ExecuteScalar();

                    message.Id = id;
                }
            }
        }

        public void UpdateMessage(Message message)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Messages
                            SET 
                                StudentId = @studenteId
                                UserProfileId = @userProfileId
                                CreateDateTime = @createDateTime
                                Body = @body
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@studentId", message.StudentId);
                    cmd.Parameters.AddWithValue("@userProfileId", message.UserProfileId);
                    cmd.Parameters.AddWithValue("@createDateTime", message.CreateDateTime);
                    cmd.Parameters.AddWithValue("@body", message.Body);
                    cmd.Parameters.AddWithValue("@id", message.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMessage(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Messages
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}