namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarsTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "HorsePower", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "NumberOfDoors", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "NumberOfDoors");
            DropColumn("dbo.Cars", "HorsePower");
        }
    }
}
