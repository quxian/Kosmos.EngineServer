using Kosmos.EngineServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.EngineServer.ModelDbMappings
{
    public class SeedUrlMap : EntityTypeConfiguration<SeedUrl>
    {
        public SeedUrlMap()
        {
            this.HasKey(seedUrl => seedUrl.HashCode);
            this.Property(seedUrl => seedUrl.HashCode)
                .HasMaxLength(32);
        }
    }
}
