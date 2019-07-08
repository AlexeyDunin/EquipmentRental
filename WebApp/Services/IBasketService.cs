using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public interface IBasketService
    {
        Task<BasketViewModel> GetBasket(string id);
        Task<BasketViewModel> UpdateBasket(BasketViewModel basket);

    }
}