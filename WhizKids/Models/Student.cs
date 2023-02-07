using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WhizKids.Models
{
    public class Student
    {
        public int Id { get; set; }

        [MaxLength(35)]
        public string FirstName { get; set; }

        [MaxLength(35)]
        public string LastName { get; set; }

        public int Enrolled { get; set; }

    }
}