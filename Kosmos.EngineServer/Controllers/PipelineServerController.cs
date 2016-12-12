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
    public class PipelineServerController : ApiController
    {
        private readonly AppDbContext _dbContext;

        public PipelineServerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/PipelineServer/Add")]
        public async Task<IHttpActionResult> Get(string address)
        {
            var hashCode = address.GetMD5HashCode();
            if (null != await _dbContext.PipelineServers?.FindAsync(hashCode))
                return Ok();
            _dbContext.PipelineServers.Add(new Model.PipelineServer
            {
                Address = address,
                AddressHashCode = hashCode
            });
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/PipelineServer/Delete")]
        public async Task<IHttpActionResult> Delete(string address)
        {
            var db_Model = await _dbContext.PipelineServers?.FindAsync(address.GetMD5HashCode());
            if (null == db_Model)
                return Ok();
            _dbContext.PipelineServers.Remove(db_Model);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/PipelineServer/List")]
        public async Task<IHttpActionResult> List()
        {
            return Ok(_dbContext.PipelineServers.ToList());
        }
    }
}
