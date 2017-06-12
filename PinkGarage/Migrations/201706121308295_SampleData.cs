namespace PinkGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        RegNum = c.String(nullable: false),
                        Color = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        NumOfWheels = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckOutTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkedVehicles");
        }
    }
}
