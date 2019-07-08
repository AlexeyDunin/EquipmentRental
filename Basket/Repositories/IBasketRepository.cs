using System.Threading.Tasks;

namespace Basket.Repositories
{
    public interface IBasketRepository
    {
        Task<Models.BasketModel> GetBasketAsync(string id);
        Task<Models.BasketModel> UpdateBasketAsync(Models.BasketModel basketModel);
        Task<bool> DeleteBasketAsync(string id);
    }
}