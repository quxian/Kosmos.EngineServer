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
    public class SeedUrlController : ApiController
    {
        private readonly AppDbContext _dbContext;

        public SeedUrlController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/SeedUrl/Add")]
        public async Task<IHttpActionResult> Add(string seedUrl)
        {
            var hashCode = seedUrl.GetMD5HashCode();
            if (null != await _dbContext.SeedUrls.FindAsync(hashCode))
                return Ok(seedUrl);
            var db_SeedUrl = new Model.SeedUrl
            {
                HashCode = hashCode,
                Url = seedUrl
            };
            _dbContext.SeedUrls.Add(db_SeedUrl);
            await _dbContext.SaveChangesAsync();

            return Ok(db_SeedUrl);
        }

        [HttpGet]
        [Route("api/SeedUrl/Delete")]
        public async Task<IHttpActionResult> Delete(string seedUrl)
        {
            var hashCode = seedUrl.GetMD5HashCode();

            var db_SeedUrl = await _dbContext.SeedUrls.FindAsync(hashCode);
            if (null == db_SeedUrl)
                return Ok(seedUrl);

            _dbContext.SeedUrls.Remove(db_SeedUrl);
            await _dbContext.SaveChangesAsync();

            return Ok(db_SeedUrl);
        }

        [HttpGet]
        [Route("api/SeedUrl/List")]
        public async Task<IHttpActionResult> List()
        {
            return Ok(_dbContext.SeedUrls.ToList());
        }
    }
}
