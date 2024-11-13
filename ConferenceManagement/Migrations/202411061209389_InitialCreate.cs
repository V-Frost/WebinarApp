namespace WebinarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ParticipantId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ParticipantId);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        SpeakerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.SpeakerId);
            
            CreateTable(
                "dbo.WebinarRegistrations",
                c => new
                    {
                        WebinarRegistrationId = c.Int(nullable: false, identity: true),
                        RegistrationDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        ParticipantId = c.Int(nullable: false),
                        WebinarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WebinarRegistrationId)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Webinars", t => t.WebinarId, cascadeDelete: true)
                .Index(t => t.ParticipantId)
                .Index(t => t.WebinarId);
            
            CreateTable(
                "dbo.Webinars",
                c => new
                    {
                        WebinarId = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        Date = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(precision: 7),
                        Link = c.String(),
                        Status = c.String(),
                        SpeakerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WebinarId)
                .ForeignKey("dbo.Speakers", t => t.SpeakerId, cascadeDelete: true)
                .Index(t => t.SpeakerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebinarRegistrations", "WebinarId", "dbo.Webinars");
            DropForeignKey("dbo.Webinars", "SpeakerId", "dbo.Speakers");
            DropForeignKey("dbo.WebinarRegistrations", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.Webinars", new[] { "SpeakerId" });
            DropIndex("dbo.WebinarRegistrations", new[] { "WebinarId" });
            DropIndex("dbo.WebinarRegistrations", new[] { "ParticipantId" });
            DropTable("dbo.Webinars");
            DropTable("dbo.WebinarRegistrations");
            DropTable("dbo.Speakers");
            DropTable("dbo.Participants");
        }
    }
}
