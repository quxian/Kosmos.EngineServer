using Kosmos.EngineServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmos.EngineServer.ModelDbMappings
{
    public class DownloaderServerMap : EntityTypeConfiguration<DownloaderServer>
    {
        public DownloaderServerMap()
        {
            this.HasKey(downloaderServer => downloaderServer.AddressHashCode);
            this.Property(downloaderServer => downloaderServer.AddressHashCode)
                .HasMaxLength(32);
        }
    }
}
