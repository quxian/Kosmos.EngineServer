using Kosmos.EngineServer.DbContext;
using Kosmos.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kosmos.EngineServer.Controllers
{
    public class EngineController : ApiController
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public EngineController(AppDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("api/Engine/Run")]
        public async Task<IHttpActionResult> Run()
        {
            var seedUrls = _dbContext
                .SeedUrls
                .AsParallel()
                .Select(seedUrl => new
                {
                    Value = seedUrl.Url,
                    Parent = seedUrl.Url,
                    Depth = 0
                })
                .ToList();
            var schedulerServersAddress = _dbContext
                .SchedulerServers
                .Select(schedulerServer => schedulerServer.Address)
                .ToList();

            //把url种子加入SchedulerServer
            await Task.WhenAny(schedulerServersAddress
                .AsParallel()
                .Select(async address =>
                {
                    try
                    {
                        await _httpClient.PostAsJsonAsync($"{address}api/Url", seedUrls);
                    }
                    catch (Exception e)
                    {
                        SingleHttpClient.PostException(e);
                    }
                })
                .ToArray());

            var downloaderServersAddress = _dbContext
                .DownloaderServers
                .Select(downloaderServer => downloaderServer.Address)
                .ToList();

            var processerServersAddress = _dbContext
                .ProcesserServers
                .Select(processerServer => processerServer.Address)
                .ToList();
            var pipelineServersAddress = _dbContext
                .PipelineServers
                .Select(pipelineServer => pipelineServer.Address)
                .ToList();

            //给每个下载服务器添加结果处理服务器列表
            await Task.WhenAll(downloaderServersAddress
                .AsParallel()
                .Select(async address =>
                {
                    try
                    {
                        await _httpClient.PostAsJsonAsync($"{address}api/Downloader/AddProcesserServersAddress", processerServersAddress);
                    }
                    catch (Exception e)
                    {
                        SingleHttpClient.PostException(e);
                    }
                })
                .ToArray());

            //给每个结果处理服务器添加任务调度服务器列表
            //同时添加结果保存服务器列表
            await Task.WhenAll(processerServersAddress
                .AsParallel()
                .Select(async address =>
                {
                    try
                    {
                        await _httpClient.PostAsJsonAsync($"{address}api/Processer/AddSchedulerServerAddress", schedulerServersAddress);
                        await _httpClient.PostAsJsonAsync($"{address}api/Processer/AddPipelineServerAddress", pipelineServersAddress);
                    }
                    catch (Exception e)
                    {
                        SingleHttpClient.PostException(e);
                    }
                })
                .ToArray());
            //给调度每个调度服务器添加下载服务器列表
            await Task.WhenAll(schedulerServersAddress
                .AsParallel()
                .Select(async address =>
                {
                    try
                    {
                        await _httpClient.PostAsJsonAsync($"{address}api/Scheduler/AddDownloaderServersAddress", downloaderServersAddress);
                    }
                    catch (Exception e)
                    {
                        SingleHttpClient.PostException(e);
                    }
                })
                .ToArray());

            await Task.WhenAll(schedulerServersAddress
                .AsParallel()
                .Select(async address =>
                {
                    try
                    {
                        await _httpClient.GetAsync($"{address}api/Scheduler/Run");
                    }
                    catch (Exception e)
                    {
                        SingleHttpClient.PostException(e);
                    }
                })
                .ToArray());

            return Ok("已启动");
        }

        [HttpGet]
        [Route("api/Engine/Stop")]
        public async Task<IHttpActionResult> Stop()
        {
            var schedulerServersAddress = _dbContext
                .SchedulerServers
                .Select(schedulerServer => schedulerServer.Address)
                .ToList();

            await Task.WhenAll(schedulerServersAddress.AsParallel().Select(async address =>
            {
                try
                {
                    await _httpClient.GetAsync($"{address}api/Scheduler/Stop");
                }
                catch (Exception e)
                {
                    SingleHttpClient.PostException(e);
                }
            }));

            //api/Download/CacheToDb
            var downloaderServersAddress = _dbContext
                .DownloaderServers
                .Select(downloaderServer => downloaderServer.Address)
                .ToList();
            await Task.WhenAll(downloaderServersAddress.AsParallel().Select(async address =>
            {
                try
                {
                    await _httpClient.GetAsync($"{address}api/Download/CacheToDb");
                }
                catch (Exception e)
                {
                    SingleHttpClient.PostException(e);
                }
            }));

            var pipelineServersAddress = _dbContext
                .PipelineServers
                .Select(pipelineServer => pipelineServer.Address)
                .ToList();
            await Task.WhenAll(pipelineServersAddress.AsParallel().Select(async address =>
            {
                try
                {
                    await _httpClient.GetAsync($"{address}api/Extract/CacheToDb");
                }
                catch (Exception e)
                {
                    SingleHttpClient.PostException(e);
                }
            }));

            return Ok("stopped");
        }
    }
}
