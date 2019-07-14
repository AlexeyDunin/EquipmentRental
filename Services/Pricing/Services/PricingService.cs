using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pricing.Models;

namespace Pricing.Services
{
    public class PricingService : IPricingService
    {
        private readonly IOptions<AppSettings> _settings;

        private int _premiumDays;
        private int _regularDays;
        private int _oneTimeFee;

        public PricingService(
            IOptions<AppSettings> settings)
        {
            _settings = settings;
        } 

        public int GetPrice(InventoryModel inventory)
        {
            if (inventory.RentalDays == 0) return 0;

            if (inventory.RentalDays < 0) return 0;

            Init(inventory.Type, inventory.RentalDays);
            return _oneTimeFee +
                   _premiumDays * _settings.Value.PremiumDailyFee +
                   _regularDays * _settings.Value.RegularDailyFee;
        }

        protected void Init(InventoryType type, int rentalDays)
        {
            var marketingStrategy = _settings.Value.MarketingStrategy.Find(_ => _.InventoryType == type);

            if (marketingStrategy.PremiumDays == 0)
            {
                _premiumDays = 0;
                _regularDays = rentalDays;
            }
            else if (marketingStrategy.PremiumDays < 0)
            {
                _premiumDays = rentalDays;
                _regularDays = 0;
            }
            else
            {
                _regularDays = rentalDays > marketingStrategy.PremiumDays ? rentalDays - marketingStrategy.PremiumDays : 0;
                _premiumDays = rentalDays - _regularDays;
            }

            _oneTimeFee = marketingStrategy.OneTimeFee ? _settings.Value.OneTimeFee : 0;
        }
    }
}
