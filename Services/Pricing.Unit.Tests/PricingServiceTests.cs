using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pricing.Services;
using Xunit;
using Pricing.Models;

namespace Pricing.Unit.Tests
{
    public class PricingServiceTests
    {
        private readonly IPricingService _pricingService;
        private string SettingPath => Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        private const int OneTimeFee = 100;
        private const int PremiumDailyFee = 60;
        private const int RegularDailyFee = 40;

        public PricingServiceTests()
        {
            var configuration = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(SettingPath));
            var settings = Options.Create(configuration);
            _pricingService = new PricingService(settings);
        }

        /*
            There are three different fees:
            • One-time rental fee – 100€
            • Premium daily fee – 60€/day
            • Regular daily fee – 40€/day

            The price calculation for different types of equipment is:
            • Heavy – rental price is one-time rental fee plus premium fee for each day rented.
            • Regular – rental price is one-time rental fee plus premium fee for the first 2 days each plus regular fee for the number of days over 2.
            • Specialized – rental price is premium fee for the first 3 days each plus regular fee times the number of days over 3.
        */

        [Theory]
        [InlineData(InventoryType.Heavy, 0, 0)]
        public void GetPrice(InventoryType inventoryType, int rentalDays, int expectedPrice)
        {
            var actualPrice = _pricingService.GetPrice(new InventoryModel
            {
                Type = inventoryType,
                RentalDays = rentalDays
            });

            Assert.Equal(expectedPrice, actualPrice);
        }
    }
}
