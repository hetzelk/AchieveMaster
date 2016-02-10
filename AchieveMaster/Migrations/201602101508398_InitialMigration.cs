namespace AchieveMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Category = c.String(),
                        Description = c.String(),
                        StudentLocation = c.String(),
                        MeetLocation = c.String(),
                        Expired = c.String(),
                        PayRate = c.String(),
                        Image = c.String(),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Requests");
        }
    }
}
