using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkGarage.Models {

    public class ParkedVehicle {

        public int ID { get; set; }
        [Required]
        public string RegNum { get; set; }

        public Type Type { get; set; }

        [Required]
        [StringLength (1024)]
        public string Color { get; set; }

        [Required]
        [StringLength(1024)]
        public string Brand { get; set; }

        [Required]
        [StringLength(1024)]
        public string Model { get; set; }

        [Range(1, 10)]
        [Required]
        public int NumOfWheels { get; set; }

        public DateTime CheckInTime { get; set; }

    }
    public enum Type {
        Car,
        Bus,
        MotorCycle
    }


}