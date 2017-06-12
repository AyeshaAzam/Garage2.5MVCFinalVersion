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
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NumOfWheels { get; set; }
        public DateTime CheckInTime { get; set; }

    }
    public enum Type {
        Car,
        Bus,
        MotorCycle
    }


}