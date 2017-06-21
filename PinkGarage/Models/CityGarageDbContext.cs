using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PinkGarage.Models
{
    public class CityGarageDbContext : DbContext
    {
        public CityGarageDbContext() : base("CityGarageDbContext")
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
    }
}