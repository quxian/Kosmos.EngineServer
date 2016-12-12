using Kosmos.EngineServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.EngineServer.ModelDbMappings
{
    public class PipelineServerMap : EntityTypeConfiguration<PipelineServer>
    {
        public PipelineServerMap()
        {
            this.HasKey(pipelineServer => pipelineServer.AddressHashCode);
            this.Property(pipelineServer => pipelineServer.AddressHashCode)
                .HasMaxLength(32);
        }
    }
}
