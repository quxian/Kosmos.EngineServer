namespace Kosmos.EngineServer.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_SeedUrl_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeedUrls",
                c => new
                    {
                        HashCode = c.String(nullable: false, maxLength: 32),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.HashCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SeedUrls");
        }
    }
}
