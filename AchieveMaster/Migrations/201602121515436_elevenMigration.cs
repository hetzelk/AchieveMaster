namespace AchieveMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elevenMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "NewMessage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "NewMessage");
        }
    }
}
