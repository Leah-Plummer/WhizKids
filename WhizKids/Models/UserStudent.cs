
using System.Collections.Generic;

namespace WhizKids.Models
{
    public class UserStudent
    {
        public int Id { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<UserProfile> UserProfiles { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public List<Student> Students { get; set; }

    }
}