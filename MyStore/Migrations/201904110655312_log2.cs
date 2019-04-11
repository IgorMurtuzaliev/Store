namespace MyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class log2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MyLoggers", "RequestTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MyLoggers", "RequestTime", c => c.DateTime(nullable: false));
        }
    }
}
