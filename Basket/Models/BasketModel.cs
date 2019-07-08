using System.Collections.Generic;

namespace Basket.Models
{
    public class BasketModel
    {
        public string Id { get; set; }
        public List<Item> Items { get; set; }

        public BasketModel(string id)
        {
            Id = id;
            Items = new List<Item>();
        }
    }
}
