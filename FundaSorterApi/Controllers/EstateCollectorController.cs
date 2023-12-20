using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using FundaSorterApi.Client;
using FundaSorterApi.Models.Response;
using FundaSorterApi.Models.Requests;
using NRedisStack.RedisStackCommands;
using System.Text.Json;

namespace FundaSorterApi.Controllers
{
    [ApiController]
    [Route("/api/v2/[controller]")]
    public class EstateCollectorController : ControllerBase
    {
        private IDatabase _cacheDB;
        private readonly ILogger<EstateCollectorController> _logger;

        public EstateCollectorController(ILogger<EstateCollectorController> logger)
        {
            _logger = logger;
            _cacheDB = CacheConnection.Connection.GetDatabase();
            _logger.LogInformation("EstateCollectorController instantiated at {Time}", DateTime.Now);
        }

        [HttpPost("RetrieveRealEstatesFromMessage")]
        public async Task<IActionResult> retrieveEstatesFromMessage([FromBody] KoopResponseDTO koopResponse)
        {
            long entryCount = 0;
            try
            {
                entryCount = await populateSortedCityObjects(koopResponse);
                _logger.LogInformation("retrieveEstatesFromMessage retrieved " +
                    "koopResponse(TODO: give object detail) at {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in retrieveEstatesFromMessage exception:" +
                    " {0} at {Time}", ex.Message, DateTime.Now);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(entryCount);
        }

        [HttpPost("RetrieveAllRealEstatesFromFunda")]
        public async Task<IActionResult> retrieveAllEstatesFromFunda([FromBody]FilterRequestDTO filterRequest)
        {
            PropertyCollectorClient client = new PropertyCollectorClient();
            long entryCount = 0;
            int currentPage = 1;
            int totalPages = 1;
            try
            {
                KoopResponseDTO koopResponse;
                while (totalPages >= currentPage)
                {

                    koopResponse = await PropertyCollectorClient
                        .fethPageAsync(currentPage,filterRequest.CityName, filterRequest.SearchFor);
                    if (koopResponse.Paging != null)
                    {
                        totalPages = koopResponse.Paging.AantalPaginas;
                        currentPage = koopResponse.Paging.HuidigePagina + 1;
                    }
                    
                    entryCount += await populateSortedCityObjects(koopResponse);
                } 
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in retrieveAllEstatesFromFunda exception:" +
                    " {0} at {Time}", ex.Message, DateTime.Now);
                return StatusCode(500, "Internal Server Error");
            }
            _logger.LogInformation("retrieveAllEstatesFromFunda retrieved with respect to" +
                " filterRequest(TODO: give object detail) at {Time}", DateTime.Now);
            return Ok(entryCount);
        }

        [HttpPost("CacheInDataFromFunda-Experimental")]
        public async Task<IActionResult> cacheInDataFromFunda([FromBody] FilterRequestDTO filterRequest)
        {
            long entryCount = 0;
            int currentPage = 1;
            int totalPages = 1;
            try
            {
                KoopResponseDTO koopResponse;
                while (totalPages >= currentPage)
                {

                    koopResponse = await PropertyCollectorClient
                        .fethPageAsync(currentPage, filterRequest.CityName, filterRequest.SearchFor);
                    if (koopResponse.Paging != null)
                    {
                        totalPages = koopResponse.Paging.AantalPaginas;
                        currentPage = koopResponse.Paging.HuidigePagina;
                    }

                    entryCount += await populateIntoCache(koopResponse, filterRequest.CityName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in cacheInDataFromFunda exception: {0} at {Time}", ex.Message, DateTime.Now);
                return StatusCode(500, "Internal Server Error");
            }
            _logger.LogInformation("cacheInDataFromFunda cached total of {0} " +
                "with respect to filterRequest(TODO: give object detail) at {Time}", entryCount, DateTime.Now);
            return Ok(entryCount);
        }

        [HttpGet("GetTopTenMakelaars")]
        public async Task<IActionResult> GetResult()
        {
            try 
            {
                var rawData = await _cacheDB.SortedSetRangeByRankWithScoresAsync("MakelaarsSorted",0, 9, Order.Descending);
                var resultData = new Dictionary<string, int>();
                foreach(var entry in rawData)
                {
                    resultData[entry.Element] = (int)entry.Score;
                }
                _logger.LogInformation("GetResult returns MakelaarsSorted(TODO: give object detail) at {Time}", DateTime.Now);
                return Ok(resultData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in GetResult exception: {0} at {Time}", ex.Message, DateTime.Now);
                return StatusCode(500, "Internal Server Error");
            }
        }

        private async Task<int> populateSortedCityObjects(KoopResponseDTO koopResponse)
        {
            int entryCount = 0;
            if (koopResponse != null && koopResponse.Objects != null)
            {
                foreach (RealEstateDTO property in koopResponse.Objects)
                {
                    await _cacheDB.SortedSetIncrementAsync("MakelaarsSorted", property.MakelaarNaam, 1);
                }
                entryCount = koopResponse.Objects.Count;
            }
            _logger.LogInformation("populateSortedCityObjects created sorted cache object at {Time}", DateTime.Now);
            return entryCount;
        }

        private async Task<int> populateIntoCache(KoopResponseDTO koopResponse, string cityName)
        {
            int entryCount = 0;
            if (koopResponse != null && koopResponse.Objects != null)
            {
                foreach (RealEstateDTO property in koopResponse.Objects)
                {
                    await _cacheDB.JSON().ArrAppendAsync(property.MakelaarNaam, JsonSerializer.Serialize(property));
                }
            }
            _logger.LogInformation("populateIntoCache cached {0} objects at {Time}", entryCount, DateTime.Now);
            return entryCount;
        }
    }
}
