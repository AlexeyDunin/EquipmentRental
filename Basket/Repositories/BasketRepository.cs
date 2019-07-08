using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Basket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private static ConcurrentDictionary<string, Models.BasketModel> _repository;

        public BasketRepository()
        {
            if(_repository == null)
                _repository = new ConcurrentDictionary<string, Models.BasketModel>();
        }

        public async Task<Models.BasketModel> GetBasketAsync(string id)
        {
            var basket = new Models.BasketModel(id);
            return _repository.GetOrAdd(id, key => basket);
        }

        public async Task<Models.BasketModel> UpdateBasketAsync(Models.BasketModel value)
        {
            var success = false;
            if (_repository.TryGetValue(value.Id, out var basket))
            {
                success = _repository.TryUpdate(value.Id, value, basket);
            }

            return success ? value : null;
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
