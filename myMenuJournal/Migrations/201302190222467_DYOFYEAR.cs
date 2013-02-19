namespace myMenuJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DYOFYEAR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDays", "DOYIntake", c => c.Int(nullable: false));
            DropColumn("dbo.UserDays", "DateIntake");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDays", "DateIntake", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserDays", "DOYIntake");
        }
    }
}
