namespace AchieveMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourteenMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "StudentLocation", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "MeetLocation", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "PayRate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "PayRate", c => c.String());
            AlterColumn("dbo.Requests", "MeetLocation", c => c.String());
            AlterColumn("dbo.Requests", "StudentLocation", c => c.String());
            AlterColumn("dbo.Requests", "Description", c => c.String());
            AlterColumn("dbo.Requests", "Category", c => c.String());
            AlterColumn("dbo.Requests", "Title", c => c.String());
        }
    }
}
