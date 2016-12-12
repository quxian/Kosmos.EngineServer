using Kosmos.EngineServer.DbContext;
using Kosmos.EngineServer.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using StringExtensionForYongsheng;
using System.Linq;

namespace Kosmos.EngineServer.Controllers
{
    public class DownloaderServerController : ApiController
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public DownloaderServerController(AppDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("api/DownloaderServer/Add")]
        public async Task<IHttpActionResult> Get(string address)
        {
            var hashCode = address.GetMD5HashCode();
            if (null != await _dbContext.DownloaderServers?.FindAsync(hashCode))
                return Ok();

            _dbContext.DownloaderServers.Add(new DownloaderServer
            {
                Address = address,
                AddressHashCode = hashCode
            });
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/DownloaderServer/Delete")]
        public async Task<IHttpActionResult> Delete(string address)
        {
            var hashCode = address.GetMD5HashCode();
            var downloaderServer = await _dbContext.DownloaderServers?.FindAsync(hashCode);
            if (null != downloaderServer)
            {
                _dbContext.DownloaderServers.Remove(downloaderServer);
                await _dbContext.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/DownloaderServer/List")]
        public async Task<IHttpActionResult> List()
        {
            var list = _dbContext.DownloaderServers.ToList();
            return Ok(list);
        }
    }
}
