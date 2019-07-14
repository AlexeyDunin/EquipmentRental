using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class BasketViewModel
    {
        public string Id { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}