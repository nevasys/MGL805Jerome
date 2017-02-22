namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarsTable3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Division = c.String(),
                        AffiliatedWith = c.String(),
                        CivicNumber = c.Int(nullable: false),
                        StreetName = c.String(),
                        Suite = c.String(),
                        City = c.String(),
                        Province = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        PostalCode = c.String(),
                        TelephoneNumber = c.String(),
                        FaxNumber = c.String(),
                        Email = c.String(),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "DailyRate", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "DailyRate");
            DropTable("dbo.Agencies");
        }
    }
}
