using Kosmos.EngineServer.DbContext;
using StringExtensionForYongsheng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kosmos.EngineServer.Controllers
{
    public class ProcesserServerController : ApiController
    {
        private readonly AppDbContext _dbContext;

        public ProcesserServerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/ProcesserServer/Add")]
        public async Task<IHttpActionResult> Get(string address)
        {
            var hashCode = address.GetMD5HashCode();
            if (null != await _dbContext.ProcesserServers.FindAsync(hashCode))
                return Ok();
            _dbContext.ProcesserServers.Add(new Model.ProcesserServer
            {
                Address = address,
                AddressHashCode = hashCode
            });
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/ProcesserServer/Delete")]
        public async Task<IHttpActionResult> Delete(string address)
        {
            var hashCode = address.GetMD5HashCode();
            var processerServer = await _dbContext.ProcesserServers.FindAsync(hashCode);
            if (null == processerServer)
                return Ok();

            _dbContext.ProcesserServers.Remove(processerServer);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        [Route("api/ProcesserServer/List")]
        public async Task<IHttpActionResult> List()
        {
            return Ok(_dbContext.ProcesserServers.ToList());
        }
    }
}
