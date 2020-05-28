namespace MarathonSkills.WpfApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Charity",
                c => new
                    {
                        CharityId = c.Int(nullable: false, identity: true),
                        CharityName = c.String(nullable: false, maxLength: 100),
                        CharityDescription = c.String(maxLength: 2000),
                        CharityLogo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CharityId);
            
            CreateTable(
                "dbo.Registration",
                c => new
                    {
                        RegistrationId = c.Int(nullable: false, identity: true),
                        RunnerId = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        RaceKitOptionId = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        RegistrationStatusId = c.Byte(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 10, scale: 2),
                        CharityId = c.Int(nullable: false),
                        SponsorshipTarget = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.RegistrationId)
                .ForeignKey("dbo.RaceKitOption", t => t.RaceKitOptionId)
                .ForeignKey("dbo.Runner", t => t.RunnerId)
                .ForeignKey("dbo.RegistrationStatus", t => t.RegistrationStatusId)
                .ForeignKey("dbo.Charity", t => t.CharityId)
                .Index(t => t.RunnerId)
                .Index(t => t.RaceKitOptionId)
                .Index(t => t.RegistrationStatusId)
                .Index(t => t.CharityId);
            
            CreateTable(
                "dbo.RaceKitOption",
                c => new
                    {
                        RaceKitOptionId = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        RaceKitOption = c.String(nullable: false, maxLength: 80),
                        Cost = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.RaceKitOptionId);
            
            CreateTable(
                "dbo.RegistrationEvent",
                c => new
                    {
                        RegistrationEventId = c.Int(nullable: false, identity: true),
                        RegistrationId = c.Int(nullable: false),
                        EventId = c.String(nullable: false, maxLength: 6, fixedLength: true),
                        BibNumber = c.Short(),
                        RaceTime = c.Int(),
                    })
                .PrimaryKey(t => t.RegistrationEventId)
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.Registration", t => t.RegistrationId)
                .Index(t => t.RegistrationId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.String(nullable: false, maxLength: 6, fixedLength: true),
                        EventName = c.String(nullable: false, maxLength: 50),
                        EventTypeId = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        MarathonId = c.Byte(nullable: false),
                        StartDateTime = c.DateTime(),
                        Cost = c.Decimal(precision: 10, scale: 2),
                        MaxParticipants = c.Short(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.EventType", t => t.EventTypeId)
                .ForeignKey("dbo.Marathon", t => t.MarathonId)
                .Index(t => t.EventTypeId)
                .Index(t => t.MarathonId);
            
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        EventTypeId = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        EventTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
            CreateTable(
                "dbo.Marathon",
                c => new
                    {
                        MarathonId = c.Byte(nullable: false, identity: true),
                        MarathonName = c.String(nullable: false, maxLength: 80),
                        CityName = c.String(maxLength: 80),
                        CountryCode = c.String(nullable: false, maxLength: 3, fixedLength: true),
                        YearHeld = c.Short(),
                    })
                .PrimaryKey(t => t.MarathonId)
                .ForeignKey("dbo.Country", t => t.CountryCode)
                .Index(t => t.CountryCode);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryCode = c.String(nullable: false, maxLength: 3, fixedLength: true),
                        CountryName = c.String(nullable: false, maxLength: 100),
                        CountryFlag = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CountryCode);
            
            CreateTable(
                "dbo.Runner",
                c => new
                    {
                        RunnerId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100),
                        Gender = c.String(nullable: false, maxLength: 10),
                        DateOfBirth = c.DateTime(),
                        CountryCode = c.String(nullable: false, maxLength: 3, fixedLength: true),
                    })
                .PrimaryKey(t => t.RunnerId)
                .ForeignKey("dbo.Gender", t => t.Gender)
                .ForeignKey("dbo.User", t => t.Email)
                .ForeignKey("dbo.Country", t => t.CountryCode)
                .Index(t => t.Email)
                .Index(t => t.Gender)
                .Index(t => t.CountryCode);
            
            CreateTable(
                "dbo.Gender",
                c => new
                    {
                        Gender = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Gender);
            
            CreateTable(
                "dbo.Volunteer",
                c => new
                    {
                        VolunteerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 80),
                        LastName = c.String(maxLength: 80),
                        CountryCode = c.String(nullable: false, maxLength: 3, fixedLength: true),
                        Gender = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.VolunteerId)
                .ForeignKey("dbo.Gender", t => t.Gender)
                .ForeignKey("dbo.Country", t => t.CountryCode)
                .Index(t => t.CountryCode)
                .Index(t => t.Gender);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(maxLength: 80),
                        LastName = c.String(maxLength: 80),
                        RoleId = c.String(nullable: false, maxLength: 1, fixedLength: true),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.RegistrationStatus",
                c => new
                    {
                        RegistrationStatusId = c.Byte(nullable: false, identity: true),
                        RegistrationStatus = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.RegistrationStatusId);
            
            CreateTable(
                "dbo.Sponsorship",
                c => new
                    {
                        SponsorshipId = c.Int(nullable: false, identity: true),
                        SponsorName = c.String(nullable: false, maxLength: 150),
                        RegistrationId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.SponsorshipId)
                .ForeignKey("dbo.Registration", t => t.RegistrationId)
                .Index(t => t.RegistrationId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false, maxLength: 50, unicode: false),
                        PositionDescription = c.String(maxLength: 1000, unicode: false),
                        PayPeriod = c.String(nullable: false, maxLength: 10, unicode: false),
                        Payrate = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 80, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 80, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateOfDBirth = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 10, unicode: false),
                        PositionId = c.Int(nullable: false),
                        Comments = c.String(maxLength: 2000, unicode: false),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.Position", t => t.PositionId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Timesheet",
                c => new
                    {
                        TimesheetId = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(),
                        EndDateTime = c.DateTime(),
                        PayAmount = c.Decimal(precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.TimesheetId)
                .ForeignKey("dbo.Staff", t => t.StaffId)
                .Index(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Timesheet", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Registration", "CharityId", "dbo.Charity");
            DropForeignKey("dbo.Sponsorship", "RegistrationId", "dbo.Registration");
            DropForeignKey("dbo.Registration", "RegistrationStatusId", "dbo.RegistrationStatus");
            DropForeignKey("dbo.RegistrationEvent", "RegistrationId", "dbo.Registration");
            DropForeignKey("dbo.RegistrationEvent", "EventId", "dbo.Event");
            DropForeignKey("dbo.Event", "MarathonId", "dbo.Marathon");
            DropForeignKey("dbo.Volunteer", "CountryCode", "dbo.Country");
            DropForeignKey("dbo.Runner", "CountryCode", "dbo.Country");
            DropForeignKey("dbo.Runner", "Email", "dbo.User");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Registration", "RunnerId", "dbo.Runner");
            DropForeignKey("dbo.Volunteer", "Gender", "dbo.Gender");
            DropForeignKey("dbo.Runner", "Gender", "dbo.Gender");
            DropForeignKey("dbo.Marathon", "CountryCode", "dbo.Country");
            DropForeignKey("dbo.Event", "EventTypeId", "dbo.EventType");
            DropForeignKey("dbo.Registration", "RaceKitOptionId", "dbo.RaceKitOption");
            DropIndex("dbo.Timesheet", new[] { "StaffId" });
            DropIndex("dbo.Staff", new[] { "PositionId" });
            DropIndex("dbo.Sponsorship", new[] { "RegistrationId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.Volunteer", new[] { "Gender" });
            DropIndex("dbo.Volunteer", new[] { "CountryCode" });
            DropIndex("dbo.Runner", new[] { "CountryCode" });
            DropIndex("dbo.Runner", new[] { "Gender" });
            DropIndex("dbo.Runner", new[] { "Email" });
            DropIndex("dbo.Marathon", new[] { "CountryCode" });
            DropIndex("dbo.Event", new[] { "MarathonId" });
            DropIndex("dbo.Event", new[] { "EventTypeId" });
            DropIndex("dbo.RegistrationEvent", new[] { "EventId" });
            DropIndex("dbo.RegistrationEvent", new[] { "RegistrationId" });
            DropIndex("dbo.Registration", new[] { "CharityId" });
            DropIndex("dbo.Registration", new[] { "RegistrationStatusId" });
            DropIndex("dbo.Registration", new[] { "RaceKitOptionId" });
            DropIndex("dbo.Registration", new[] { "RunnerId" });
            DropTable("dbo.Timesheet");
            DropTable("dbo.Staff");
            DropTable("dbo.Position");
            DropTable("dbo.Sponsorship");
            DropTable("dbo.RegistrationStatus");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Volunteer");
            DropTable("dbo.Gender");
            DropTable("dbo.Runner");
            DropTable("dbo.Country");
            DropTable("dbo.Marathon");
            DropTable("dbo.EventType");
            DropTable("dbo.Event");
            DropTable("dbo.RegistrationEvent");
            DropTable("dbo.RaceKitOption");
            DropTable("dbo.Registration");
            DropTable("dbo.Charity");
        }
    }
}
