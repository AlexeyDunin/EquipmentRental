using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApp.Infrastructure;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BasketService> _logger;

        private readonly string _basketUrl;

        public BasketService(HttpClient httpClient, ILogger<BasketService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;

            _basketUrl = $"{settings.Value.BaseUrl}/api/v1/basket";
        }

        public async Task<BasketViewModel> GetBasket(string id)
        {
            var uri = Api.Basket.GetBasket(_basketUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            return string.IsNullOrEmpty(responseString) ?
                new BasketViewModel { Id = id } :
                JsonConvert.DeserializeObject<BasketViewModel>(responseString);
        }

        public async Task<BasketViewModel> UpdateBasket(BasketViewModel basket)
        {
            var uri = Api.Basket.UpdateBasket(_basketUrl);

            var basketContent = new StringContent(JsonConvert.SerializeObject(basket), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, basketContent);

            response.EnsureSuccessStatusCode();

            return basket;
        }
    }
}