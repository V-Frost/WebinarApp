namespace WebinarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Webinars", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Webinars", "Category");
        }
    }
}
