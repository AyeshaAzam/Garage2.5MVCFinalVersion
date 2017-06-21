namespace PinkGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleTypes", "VehicleTypeName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleTypes", "VehicleTypeName", c => c.Int(nullable: false));
        }
    }
}
