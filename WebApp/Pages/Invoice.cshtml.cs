using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using WebApp.Repositories;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Pages
{
    public class InvoiceModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly IInvoiceService _invoiceService;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IOptions<AppSettings> _settings;

        public InvoiceModel(
            IBasketService basketService,
            IInvoiceService invoiceService,
            IInventoryRepository inventoryRepository,
            IOptions<AppSettings> settings)
        {
            _basketService = basketService;
            _invoiceService = invoiceService;
            _inventoryRepository = inventoryRepository;
            _settings = settings;
        }
        public BasketViewModel Basket { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Basket = await _basketService.GetBasket(_settings.Value.UserId);

            var inventoryList = new List<InventoryModel>();

            foreach (var item in Basket.Items)
            {
                var inventory = _inventoryRepository.GetInventoryById(item.Id);
                inventory.RentalDays = item.RentalDays;

                inventoryList.Add(inventory);
            }

            if (!inventoryList.Any()) return Page();

            var result = await _invoiceService.GetInvoice(inventoryList).Result.ReadAsStreamAsync();
            _basketService.DeleteBasket(Basket.Id);

            return File(result, "application/txt", "invoice.txt");
        }
    }
}