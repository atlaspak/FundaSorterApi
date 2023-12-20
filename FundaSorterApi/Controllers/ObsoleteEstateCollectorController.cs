using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using FundaSorterApi.Models.Response;

namespace FundaSorterApi.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ObsoleteEstateCollectorController : ControllerBase
    {
        private const int PageSize = 25;
        private StackExchange.Redis.IDatabase cacheDB;

        private readonly ILogger<ObsoleteEstateCollectorController> _logger;

        public ObsoleteEstateCollectorController(ILogger<ObsoleteEstateCollectorController> logger)
        {
            _logger = logger;
            cacheDB = CacheConnection.Connection.GetDatabase();
            
        }


        [HttpPost("RetrieveRealEstates")]
        public async Task<long> retrieveEstates([FromBody] KoopResponseDTO jsonReponse)
        {
            try
            {   if(jsonReponse != null && jsonReponse.Objects != null)
                {
                    foreach(RealEstateDTO property in jsonReponse.Objects)
                    {
                        cacheDB.HashIncrement("Makelaars", property.MakelaarId);
                    }
                }

            }
            catch (Exception ex) 
            {

            }

            return cacheDB.HashLength("Makelaars");
        }

        [HttpGet("GetMakelaars")]
        public async Task<HashEntry[]> GetResult()
        {
            return await cacheDB.HashGetAllAsync("Makelaars");
        }

        [HttpGet("GetMakelaarsSync")]
        public List<List<int>> GetResultSync()
        {
            List<List<int>> entries = cacheDB.HashGetAll("Makelaars").Select(
                    entry => new List<int>
                    {
                        (int)entry.Name,
                        (int)entry.Value
                    }).ToList();
            return entries;
        }
    }
}
