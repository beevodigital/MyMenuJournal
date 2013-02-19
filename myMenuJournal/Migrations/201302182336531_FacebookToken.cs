namespace myMenuJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacebookToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FacebookToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FacebookToken");
        }
    }
}
