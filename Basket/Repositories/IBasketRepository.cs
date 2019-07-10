using System.Threading.Tasks;
using Basket.Models;

namespace Basket.Repositories
{
    public interface IBasketRepository
    {
        Task<BasketModel> GetBasketAsync(string id);
        Task<BasketModel> UpdateBasketAsync(Models.BasketModel basketModel);
        Task<bool> DeleteBasketAsync(string id);
    }
}