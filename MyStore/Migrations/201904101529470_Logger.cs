namespace MyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyLoggers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestTime = c.DateTime(nullable: false),
                        Username = c.String(),
                        RequestUri = c.String(),
                        StatusCode = c.String(),
                        Headers = c.String(),
                        Body = c.String(),
                        QueryString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyLoggers");
        }
    }
}
