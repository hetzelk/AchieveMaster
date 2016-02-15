namespace AchieveMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eigthteenthmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "FirstPersonName", c => c.String());
            AddColumn("dbo.Messages", "SecondPersonName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "SecondPersonName");
            DropColumn("dbo.Messages", "FirstPersonName");
        }
    }
}
