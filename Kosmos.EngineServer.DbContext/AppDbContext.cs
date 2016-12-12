using Kosmos.EngineServer.Model;
using Kosmos.EngineServer.ModelDbMappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.EngineServer.DbContext
{
    public class AppDbContext : System.Data.Entity.DbContext
    {
        public AppDbContext() : base("EngineServerDbConnection") { }

        public DbSet<DownloaderServer> DownloaderServers { get; set; }
        public DbSet<ProcesserServer> ProcesserServers { get; set; }
        public DbSet<SchedulerServer> SchedulerServers { get; set; }
        public DbSet<PipelineServer> PipelineServers { get; set; }
        public DbSet<SeedUrl> SeedUrls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DownloaderServerMap());
            modelBuilder.Configurations.Add(new ProcesserServerMap());
            modelBuilder.Configurations.Add(new SchedulerServerMap());
            modelBuilder.Configurations.Add(new PipelineServerMap());
            modelBuilder.Configurations.Add(new SeedUrlMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
