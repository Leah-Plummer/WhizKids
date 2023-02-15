using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WhizKids.Models;
using WhizKids.Models.ViewModels;

namespace WhizKids.Auth.Models
{
    public class Registration
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int StudentId { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public Student Student { get; set; }
        public List<Student> Students { get; set; }
    }
}