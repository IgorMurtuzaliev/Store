namespace MyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyLoggers", "ResponseTime", c => c.String());
            AddColumn("dbo.MyLoggers", "HttpMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyLoggers", "HttpMethod");
            DropColumn("dbo.MyLoggers", "ResponseTime");
        }
    }
}
