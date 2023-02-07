namespace WhizKids.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public int CreateTime { get; set; }
        public string Body { get; set; }
    }
}
