namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarsTable4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BirthDate = c.DateTime(nullable: false),
                        HasValidDriverLicense = c.Boolean(nullable: false),
                        DriverLicenseNUmber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Agencies", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agencies", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Agencies", "CreatedBy", c => c.String());
            AddColumn("dbo.Agencies", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Agencies", "ModifiedBy", c => c.String());
            AddColumn("dbo.Cars", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "NumberOfPassenger", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cars", "CreatedBy", c => c.String());
            AddColumn("dbo.Cars", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cars", "ModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropColumn("dbo.Cars", "ModifiedBy");
            DropColumn("dbo.Cars", "ModifiedOn");
            DropColumn("dbo.Cars", "CreatedBy");
            DropColumn("dbo.Cars", "CreatedOn");
            DropColumn("dbo.Cars", "NumberOfPassenger");
            DropColumn("dbo.Cars", "IsActive");
            DropColumn("dbo.Agencies", "ModifiedBy");
            DropColumn("dbo.Agencies", "ModifiedOn");
            DropColumn("dbo.Agencies", "CreatedBy");
            DropColumn("dbo.Agencies", "CreatedOn");
            DropColumn("dbo.Agencies", "IsActive");
            DropTable("dbo.Clients");
        }
    }
}
