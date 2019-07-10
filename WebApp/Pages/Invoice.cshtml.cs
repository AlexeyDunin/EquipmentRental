using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public InvoiceModel(IBasketService basketService, IInvoiceService invoiceService, IInventoryRepository inventoryRepository)
        {
            _basketService = basketService;
            _invoiceService = invoiceService;
            _inventoryRepository = inventoryRepository;
        }
        public BasketViewModel Basket { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Basket = await _basketService.GetBasket("777");

            var inventoryList = new List<InventoryModel>();

            foreach (var item in Basket.Items)
            {
                var inventory = _inventoryRepository.GetInventoryById(item.Id);
                inventory.RentalDays = item.RentalDays;

                inventoryList.Add(inventory);
            }

            if (inventoryList.Any())
            {
                // convert string to stream
                var result = await _invoiceService.GetInvoice(inventoryList).Result.ReadAsStreamAsync();
                await _basketService.DeleteBasket(Basket.Id);

                return File(result, "application/txt", "invoice.txt");
            }

            throw new ApplicationException("Basket is empty");

           
        }
    }
}