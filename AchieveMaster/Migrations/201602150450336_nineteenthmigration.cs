namespace AchieveMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nineteenthmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rating = c.String(),
                        UserID = c.String(),
                        Review = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
