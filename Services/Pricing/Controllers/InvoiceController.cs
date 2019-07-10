using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Pricing.Models;

namespace Pricing.Controllers
{
    [Route("api/v1/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly int _oneTimeFee = 100;
        private readonly int _premiumDailyFee = 60;
        private readonly int _regularDailyFee = 40;
        private readonly int _heavyLoyaltyPoint = 2;
        private readonly int _defaultLoyaltyPoint = 1;
        private readonly int _regularSpecialPriceForDays = 2;
        private readonly int _specializedSpecialPriceForDays = 3;

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] List<InventoryModel> values)
        {
            long totalPrice = 0;
            var totalLoyaltyPoints = 0;

            var builder = new StringBuilder();
            builder.AppendLine("=== INVOICE ===");
            builder.AppendLine("Rent details");

            foreach (var value in values)
            {
                var price = GetPrice(value);
                builder.AppendLine($"{value.Name}: {price}{Currency}");
                totalPrice += price;
                totalLoyaltyPoints += GetLoyaltyPoint(value.Type);
            }

            builder.AppendLine($"Total price: {totalPrice}{Currency}");
            builder.AppendLine($"Number of bonus points earned: {totalLoyaltyPoints}");

            // convert string to stream
            var byteArray = Encoding.ASCII.GetBytes(builder.ToString());
            return File(byteArray, "application/txt", "invoice.txt");
        }

        public const string Currency = "$";

        private long GetPrice(InventoryModel value)
        {
            switch (value.Type)
            {
                case InventoryType.Heavy:
                    return _oneTimeFee + value.RentalDays * _premiumDailyFee;
                case InventoryType.Regular:
                    return _oneTimeFee + _regularSpecialPriceForDays * _premiumDailyFee +
                           (value.RentalDays - _regularSpecialPriceForDays) * _regularDailyFee;
                case InventoryType.Specialized:
                    return _premiumDailyFee * _specializedSpecialPriceForDays +
                           (value.RentalDays - _specializedSpecialPriceForDays) * _regularDailyFee;
                default: return 0;
            }
        }

        private int GetLoyaltyPoint(InventoryType type)
        {
            switch (type)
            {
                case InventoryType.Heavy:
                    return _heavyLoyaltyPoint;
                case InventoryType.Regular:
                    return _defaultLoyaltyPoint;
                case InventoryType.Specialized:
                    return _defaultLoyaltyPoint;
                default: return 0;
            }
        }
    }
}
