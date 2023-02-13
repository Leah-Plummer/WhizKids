using System;
using System.Collections.Generic;

namespace WhizKids.Models.ViewModels
{
    public class StudentProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public Student Student { get; set; }
        public List<UserProfile> UserProfiles { get; set; }
    }
}