using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Pages.Shared
{
    public class BasketModel : PageModel
    {
        private readonly IBasketService _basketService;
        public BasketModel(IBasketService basketService) => _basketService = basketService;

        private const string UserId = "777";

        public async Task OnPost([FromBody] ItemModel item)
        {
            var basket = await _basketService.GetBasket(UserId);

            basket.Items.Add(item);

            await _basketService.UpdateBasket(new BasketViewModel
            {
                Id = UserId,
                Items = basket.Items
            });
        }
    }
}