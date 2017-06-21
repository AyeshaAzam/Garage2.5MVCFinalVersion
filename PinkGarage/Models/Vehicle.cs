using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkGarage.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string RegNum { get; set; }

        [Display(Name = "Color")]
        [MaxLength(20)]
        public string Color { get; set; }

        [Display(Name = "Brand")]
        [MaxLength(20)]
        [Required]
        public string Brand { get; set; }

        public DateTime CheckInTime { get; set; }

        public int VehicleTypeId { get; set; }

        public int MemberId { get; set; }

        public virtual VehicleType VehicleType { get; set; }
        public virtual Member Member { get; set; }
    }
}