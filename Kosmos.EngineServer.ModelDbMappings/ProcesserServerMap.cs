using Kosmos.EngineServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.EngineServer.ModelDbMappings
{
    public class ProcesserServerMap : EntityTypeConfiguration<ProcesserServer>
    {
        public ProcesserServerMap()
        {
            this.HasKey(processerServer => processerServer.AddressHashCode);
            this.Property(processerServer => processerServer.AddressHashCode)
                .HasMaxLength(32);
        }
    }
}
