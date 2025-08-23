using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Data.Services
{
    public class TownSyncService : ITownSyncService
    {
        private readonly ITownRepository _townRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TownSyncService(ITownRepository townRepository, HttpClient httpClient, IConfiguration configuration)
        {
            _townRepository = townRepository;
            _httpClient = httpClient;
            _config = configuration;
        }

        public async Task SyncTownsAsync()
        {
            // 1. Configure headers (replace with your Back4App keys)
            var appId = _config["Back4App:AppId"];
            var apiKey = _config["Back4App:RestApiKey"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Parse-Application-Id", appId);
            _httpClient.DefaultRequestHeaders.Add("X-Parse-REST-API-Key", apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // 2. Call API
            var response = await _httpClient.GetAsync("https://parseapi.back4app.com/classes/City?limit=1000&where={\"country\":\"South Africa\"}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Back4App API failed: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Back4AppResponse>(json);

            // 3. Sync with DB
            var existingTowns = await _townRepository.GetAllAsync();
            var existingNames = existingTowns.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var apiTown in result.Results)
            {
                if (!existingNames.Contains(apiTown.Name))
                {
                    await _townRepository.AddAsync(new Town
                    {
                        Name = apiTown.Name,
                        Country = apiTown.Country
                    });
                }
            }
        }
    }

    public class Back4AppResponse
    {
        [JsonProperty("results")]
        public List<Back4AppTownDto> Results { get; set; }
    }
}
