using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundaSorterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private IDatabase _cacheDB;
        private readonly ILogger<EstateCollectorController> _logger;
        public DebugController(ILogger<EstateCollectorController> logger)
        {
            _logger = logger;
            _cacheDB = RedisConnectorHelper.Connection.GetDatabase();
        }

        [HttpPost("FlushDatabase")]
        public async Task<IActionResult> retrieveProperties()
        {
            try
            {
               //_cacheDB..;
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }
    }
}
