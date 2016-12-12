namespace Kosmos.EngineServer.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DownloaderServers",
                c => new
                    {
                        AddressHashCode = c.String(nullable: false, maxLength: 32),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AddressHashCode);
            
            CreateTable(
                "dbo.ProcesserServers",
                c => new
                    {
                        AddressHashCode = c.String(nullable: false, maxLength: 32),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AddressHashCode);
            
            CreateTable(
                "dbo.SchedulerServers",
                c => new
                    {
                        AddressHashCode = c.String(nullable: false, maxLength: 32),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AddressHashCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SchedulerServers");
            DropTable("dbo.ProcesserServers");
            DropTable("dbo.DownloaderServers");
        }
    }
}
