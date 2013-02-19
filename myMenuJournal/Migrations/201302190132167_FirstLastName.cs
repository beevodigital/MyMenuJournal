namespace myMenuJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProperties", "FirstName", c => c.String());
            AddColumn("dbo.UserProperties", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProperties", "LastName");
            DropColumn("dbo.UserProperties", "FirstName");
        }
    }
}
