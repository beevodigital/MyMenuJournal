namespace myMenuJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreUserChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProperties",
                c => new
                    {
                        UserPropertyId = c.Guid(nullable: false),
                        FacebookToken = c.String(),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.UserPropertyId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            DropColumn("dbo.Users", "FacebookToken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FacebookToken", c => c.String());
            DropIndex("dbo.UserProperties", new[] { "User_UserId" });
            DropForeignKey("dbo.UserProperties", "User_UserId", "dbo.Users");
            DropTable("dbo.UserProperties");
        }
    }
}
