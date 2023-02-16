using System;

namespace WhizKids.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int UserProfileId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Body { get; set; }
    }
}
