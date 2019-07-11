using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            if (!(obj is BasketModel compareWith))
            {
                return false;
            }
            
            return Id == compareWith.Id && (Items.Count == compareWith.Items.Count) && !Items.Except(compareWith.Items).Any();
        }
    }
}
