using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkGarage.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }

        public virtual ICollection<Vehicle> Vehicle { get; set; }

    }
}