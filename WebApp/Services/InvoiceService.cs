using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApp.Infrastructure;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InvoiceService> _logger;

        private readonly string _baseUrl;

        public InvoiceService(HttpClient httpClient, ILogger<InvoiceService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;

            _baseUrl = $"{settings.Value.BaseUrl}/api/v1";
        }

        public async Task<HttpContent> GetInvoice(List<InventoryModel> inventoryModels)
        {
            var uri = Api.Invoice.GetInvoice(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(inventoryModels), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            return response.Content;
        }
    }
}