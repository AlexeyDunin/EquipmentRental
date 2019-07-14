using Pricing.Models;

namespace Pricing.Services
{
    public interface IPricingService
    {
        int GetPrice(InventoryModel inventory);
    }
}
