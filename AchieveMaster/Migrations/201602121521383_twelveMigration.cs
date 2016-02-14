namespace AchieveMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twelveMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "blocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "blocked");
        }
    }
}
