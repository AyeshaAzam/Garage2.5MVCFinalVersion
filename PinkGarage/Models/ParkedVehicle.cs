using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinkGarage.Models {

    public class ParkedVehicle {

        public int ID { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string RegNum { get; set; }

        public Type Type { get; set; }

        [Display(Name = "FuelType")]
        public EngineTypes EngineType { get; set; }


        [Display(Name = "Color")]
        [MaxLength(20)]
        public string Color { get; set; }

        [Display(Name = "Brand")]
        [MaxLength (20)]
        [Required]
        public string Brand { get; set; }


        [Display(Name = "Model")]
        [MaxLength(20)]
        [Required]
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

    public enum EngineTypes
    {
        Petrol,
        Diesel,
        Electric,
        Gas,
        Hybrid
    }


}