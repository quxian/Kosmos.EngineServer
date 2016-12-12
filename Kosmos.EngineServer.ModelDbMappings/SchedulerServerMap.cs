using Kosmos.EngineServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.EngineServer.ModelDbMappings
{
    public class SchedulerServerMap : EntityTypeConfiguration<SchedulerServer>
    {
        public SchedulerServerMap()
        {
            this.HasKey(schedulerServer => schedulerServer.AddressHashCode);
            this.Property(schedulerServer => schedulerServer.AddressHashCode)
                .HasMaxLength(32);
        }
    }
}
