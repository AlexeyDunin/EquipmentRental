using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Pages.Shared
{
    public class BasketModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly IOptions<AppSettings> _setting;

        public BasketModel(IBasketService basketService, IOptions<AppSettings> settings)
        {
            _basketService = basketService;
            _setting = settings;
        }
        public BasketViewModel Basket { get; set; }

        private string UserId => _setting.Value.UserId;

        public async Task OnPost([FromBody] ItemModel item)
        {
            Basket = await _basketService.GetBasket(UserId);

            Basket.Items.Add(item);

            await _basketService.UpdateBasket(new BasketViewModel
            {
                Id = UserId,
                Items = Basket.Items
            });
        }

        public async Task OnGet()
        {
            Basket = await _basketService.GetBasket(UserId);
        }
    }
}