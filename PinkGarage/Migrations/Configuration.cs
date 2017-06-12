namespace PinkGarage.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PinkGarage.Models.GarageDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PinkGarage.Models.GarageDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.ParkedVehicles.AddOrUpdate(p => p.RegNum,
                new Models.ParkedVehicle { RegNum = "ABC123", Type = 0, EngineType = 0 , Brand = "Volvo", Model = "123", Color = "Red", NumOfWheels = 4, CheckInTime = DateTime.Now },
               new Models.ParkedVehicle { RegNum = "Car123", Type = 0, EngineType = 0, Brand = "Volvo", Model = "2017", Color = "Red", NumOfWheels = 4, CheckInTime = DateTime.Now });
              //new Models.ParkedVehicle { RegNum = "Bus123", Type = 1, Brand = "Volvo", Model = "2017", Color = "Yellow", NumOfWheels = 8, CheckInTime = DateTime.Now });
        }

    }
}
