using System;
using System.Collections.Generic;

namespace WhizKids.Models.ViewModels
{
    public class StudentProfileViewModel
    {
        public List<UserProfile> UserProfile { get; set; }
        public Student Student { get; set; }
    }
}