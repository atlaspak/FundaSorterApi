using FundaSorterApi.Models.Response;
using System.Text.Json;

namespace FundaSorterApi.Client
{
    public class PropertyCollectorClient
    {
        private static HttpClient httpClient = new HttpClient();
        private static readonly int DelayBetweenRequests = 600; // Milliseconds
        private static readonly string urlTemplate =
            "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{0}/?type=koop&zo=/{1}/{2}&page={3}&pagesize={4}";
        private static readonly string key = "76666a29898f491480386d966b75f949";
        private static readonly string page_size = "25";

        public static async Task<KoopResponseDTO> fethPageAsync(int pageNumber, string city = "amsterdam", string searchFor = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(searchFor))
                {
                    searchFor += '/';
                }
                string url = String.Format(urlTemplate, key, city, searchFor, pageNumber.ToString(), page_size);
                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                await Task.Delay(DelayBetweenRequests);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KoopResponseDTO>(responseBody);
            }
            catch (Exception ex)
            {
                return new KoopResponseDTO();
            }
        }

        //public static async Task<IAction> setParameters(string city, 
    }
}
