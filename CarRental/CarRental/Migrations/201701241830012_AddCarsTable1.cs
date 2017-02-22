namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarsTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Description", c => c.String());
            DropColumn("dbo.Cars", "Decription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Decription", c => c.String());
            DropColumn("dbo.Cars", "Description");
        }
    }
}
