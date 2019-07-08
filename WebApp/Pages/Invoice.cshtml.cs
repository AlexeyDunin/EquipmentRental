using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Pages
{
    public class InvoiceModel : PageModel
    {
        private readonly IBasketService _basketService;

        public InvoiceModel(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public BasketViewModel Basket { get; set; }

        public async Task OnGet()
        {

            Basket = await _basketService.GetBasket(HttpContext.Connection.Id);
        }
    }
}