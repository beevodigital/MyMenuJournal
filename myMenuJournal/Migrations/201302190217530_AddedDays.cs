namespace myMenuJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDays : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDays",
                c => new
                    {
                        UserDayID = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateIntake = c.DateTime(nullable: false),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserDayID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserDays", new[] { "UserID" });
            DropForeignKey("dbo.UserDays", "UserID", "dbo.Users");
            DropTable("dbo.UserDays");
        }
    }
}
