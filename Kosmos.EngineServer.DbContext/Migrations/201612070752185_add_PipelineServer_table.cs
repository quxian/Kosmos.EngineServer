namespace Kosmos.EngineServer.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_PipelineServer_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PipelineServers",
                c => new
                    {
                        AddressHashCode = c.String(nullable: false, maxLength: 32),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AddressHashCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PipelineServers");
        }
    }
}
