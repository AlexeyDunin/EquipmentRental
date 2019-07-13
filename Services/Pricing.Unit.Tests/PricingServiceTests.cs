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

        public PricingServiceTests()
        {
            var configuration = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(SettingPath));
            var settings = Options.Create(configuration);
            _pricingService = new PricingService(settings);
        }

        private const int OneTimeFee = 100;
        private const int PremiumDailyFee = 60;
        private const int RegularDailyFee = 40;

        // xUnit has a issue when inline data is an enum https://github.com/xunit/xunit/issues/1971

        // Heavy – rental price is one-time rental fee plus premium fee for each day rented.
        [Theory]
        [InlineData(1, OneTimeFee + PremiumDailyFee)]
        [InlineData(3, OneTimeFee + 3 * PremiumDailyFee)]
        public void GetPrice_Heavy(int rentalDays, int expectedPrice)
        {
            GetPriceAssertion(InventoryType.Heavy, rentalDays, expectedPrice);
        }

        // Regular – rental price is one-time rental fee plus premium fee for the first 2 days each plus regular fee for the number of days over 2.
        [Theory]
        [InlineData(2, OneTimeFee + 2 * PremiumDailyFee)]
        [InlineData(5, OneTimeFee + 2 * PremiumDailyFee + 3 * RegularDailyFee)]
        public void GetPrice_Regular(int rentalDays, int expectedPrice)
        {
            GetPriceAssertion(InventoryType.Regular, rentalDays, expectedPrice);
        }

        // Specialized – rental price is premium fee for the first 3 days each plus regular fee times the number of days over 3.
        [Theory]
        [InlineData(1, 1 * PremiumDailyFee)]
        [InlineData(4, 3 * PremiumDailyFee + 1 * RegularDailyFee)]
        public void GetPrice_Specialized(int rentalDays, int expectedPrice)
        {
            GetPriceAssertion(InventoryType.Specialized, rentalDays, expectedPrice);
        }

        [Theory]
        [InlineData(0, 0)]
        public void GetPrice_When_RentalDays_Is_Zero(int rentalDays, int expectedPrice)
        {
            GetPriceAssertion(InventoryType.Specialized, rentalDays, expectedPrice);
        }

        [Theory]
        [InlineData(-1, 0)]
        public void GetPrice_When_RentalDays_Is_Negative(int rentalDays, int expectedPrice)
        {
            GetPriceAssertion(InventoryType.Specialized, rentalDays, expectedPrice);
        }

        private void GetPriceAssertion(InventoryType inventoryType, int rentalDays, int expectedPrice)
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

