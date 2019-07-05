using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApp.Infrastructure;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InventoryService> _logger;

        private readonly string _remoteServiceBaseUrl;

        public InventoryService(HttpClient httpClient, ILogger<InventoryService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;

            _remoteServiceBaseUrl = $"{settings.Value.BaseUrl}/api/v1";
        }

        public  List<InventoryModel> GetItems()
        {
            var uri = Api.Inventory.GetAllCatalogItems(_remoteServiceBaseUrl);

            var responseString =  _httpClient.GetStringAsync(uri);

            var catalog = JsonConvert.DeserializeObject<List<InventoryModel>>(responseString.Result);

            return catalog;
        }
    }
}