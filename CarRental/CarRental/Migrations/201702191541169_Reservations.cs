namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "AgencyId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "ProvincialTax", c => c.Single(nullable: false));
            AddColumn("dbo.Reservations", "FederalTax", c => c.Single(nullable: false));
            AddColumn("dbo.Reservations", "Amount", c => c.Single(nullable: false));
            AddColumn("dbo.Reservations", "TotalAmount", c => c.Single(nullable: false));
            CreateIndex("dbo.Reservations", "AgencyId");
            AddForeignKey("dbo.Reservations", "AgencyId", "dbo.Agencies", "Id", cascadeDelete: false);
            DropColumn("dbo.Reservations", "Tax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Tax", c => c.Single(nullable: false));
            DropForeignKey("dbo.Reservations", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.Reservations", new[] { "AgencyId" });
            DropColumn("dbo.Reservations", "TotalAmount");
            DropColumn("dbo.Reservations", "Amount");
            DropColumn("dbo.Reservations", "FederalTax");
            DropColumn("dbo.Reservations", "ProvincialTax");
            DropColumn("dbo.Reservations", "AgencyId");
        }
    }
}
