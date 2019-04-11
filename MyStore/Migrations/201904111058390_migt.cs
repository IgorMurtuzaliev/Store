namespace MyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyLoggers", "StackTrace", c => c.String());
            AddColumn("dbo.MyLoggers", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyLoggers", "Message");
            DropColumn("dbo.MyLoggers", "StackTrace");
        }
    }
}
