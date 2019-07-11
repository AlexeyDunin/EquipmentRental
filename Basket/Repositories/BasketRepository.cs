using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Basket.Models;

namespace Basket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private static ConcurrentDictionary<string, BasketModel> _repository;

        public BasketRepository()
        {
            if(_repository == null)
                _repository = new ConcurrentDictionary<string, BasketModel>();
        }

        public async Task<BasketModel> GetBasketAsync(string id)
        {
            var basket = new BasketModel(id);
            return _repository.GetOrAdd(id, key => basket);
        }

        public async Task<BasketModel> UpdateBasketAsync(BasketModel value)
        {
            var success = false;
            if (_repository.TryGetValue(value.Id, out var basket))
            {
                success = _repository.TryUpdate(value.Id, GetMergedBasketModel(value), basket);
            }

            return success ? value : null;
        }

        private BasketModel GetMergedBasketModel(BasketModel value)
        {
            value.Items = value.Items.Select(_ => _.Id)
                .Distinct()
                .Select(i => new Item
                {
                    Id = i,
                    RentalDays = value.Items
                        .Where(_ => _.Id.Equals(i))
                        .Select(v => v.RentalDays).Sum()
                })
                .ToList();
            return value;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return _repository.TryRemove(id, out var basket);
        }

        public void Clear()
        {
            _repository.Clear();
        }
    }
}
