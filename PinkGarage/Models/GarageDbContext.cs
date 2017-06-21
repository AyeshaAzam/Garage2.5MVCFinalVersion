using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PinkGarage.Models {
    public class GarageDbContext : DbContext {
        public GarageDbContext() : base("GarageDbConnection") {

        }

        public DbSet<Models.ParkedVehicle> ParkedVehicles { get; set; }

        public System.Data.Entity.DbSet<PinkGarage.Models.Vehicle> Vehicles { get; set; }

        public System.Data.Entity.DbSet<PinkGarage.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<PinkGarage.Models.VehicleType> VehicleTypes { get; set; }
    }
}