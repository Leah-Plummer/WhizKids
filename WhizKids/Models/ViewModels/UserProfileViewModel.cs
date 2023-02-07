using System;
using System.Collections.Generic;

namespace WhizKids.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Student> Students { get; set; }
    }
}