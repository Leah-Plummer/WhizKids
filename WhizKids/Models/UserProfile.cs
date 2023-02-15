using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WhizKids.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        //[Required]
        public string FirebaseUserId { get; set; }

        //[Required(ErrorMessage = "add a first name...")]
        //[MaxLength(35)]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "add a last name...")]
        //[MaxLength(35)]
        public string LastName { get; set; }

        public string Email { get; set; }

        //[Required]
        //[StringLength(55, MinimumLength = 5)]
        public string Address { get; set; }

        //[Phone]
        //[DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public int IsAdmin { get; set; }

        public int StudentId { get; set; }
        public List<Student> Students { get; set; }



    }
}