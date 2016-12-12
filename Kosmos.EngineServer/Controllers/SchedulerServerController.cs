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
    public class SchedulerServerController : ApiController
    {
        private readonly AppDbContext _dbContext;

        public SchedulerServerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/SchedulerServer/Add")]
        public async Task<IHttpActionResult> Get(string address)
        {
            var hashCode = address.GetMD5HashCode();
            if (null != await _dbContext.SchedulerServers?.FindAsync(hashCode))
                return Ok();

            _dbContext.SchedulerServers.Add(new Model.SchedulerServer
            {
                Address = address,
                AddressHashCode = hashCode
            });
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        [Route("api/SchedulerServer/Delete")]
        public async Task<IHttpActionResult> Delete(string address)
        {
            var hashCode = address.GetMD5HashCode();

            var schedulerServer = await _dbContext.SchedulerServers.FindAsync(hashCode);
            if (null == schedulerServer)
                return Ok();
            _dbContext.SchedulerServers.Remove(schedulerServer);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/SchedulerServer/List")]
        public async Task<IHttpActionResult> List()
        {
            return Ok(_dbContext.SchedulerServers.ToList());
        }
    }
}
