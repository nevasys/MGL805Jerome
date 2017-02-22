namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarInventoriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarInventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        AgencyId = c.Int(nullable: false),
                        SerialNumber = c.String(),
                        Odometer = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        AirConditionner = c.Boolean(nullable: false),
                        DVDplayer = c.Boolean(nullable: false),
                        MP3player = c.Boolean(nullable: false),
                        NavigationSystem = c.Boolean(nullable: false),
                        IsReserved = c.Boolean(nullable: false),
                        DailyRate = c.Single(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, cascadeDelete: true)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.AgencyId);
            
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarInventoryId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        OdometerStart = c.Int(nullable: false),
                        OdometerEnd = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        DailyRate = c.Single(nullable: false),
                        Tax = c.Single(nullable: false),
                        Discount = c.Single(nullable: false),
                        ReservationStatus = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarInventories", t => t.CarInventoryId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.CarInventoryId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Reservations", "CarInventoryId", "dbo.CarInventories");
            DropForeignKey("dbo.CarInventories", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CarInventories", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.Reservations", new[] { "ClientId" });
            DropIndex("dbo.Reservations", new[] { "CarInventoryId" });
            DropIndex("dbo.CarInventories", new[] { "AgencyId" });
            DropIndex("dbo.CarInventories", new[] { "CarId" });
            DropTable("dbo.Reservations");
            DropTable("dbo.FAQs");
            DropTable("dbo.CarInventories");
        }
    }
}
