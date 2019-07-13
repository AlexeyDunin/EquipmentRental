using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pricing.Models;
using Pricing.Services;

namespace Pricing.Controllers
{
    [Route("api/v1/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IPricingService _pricingService;

        public InvoiceController(
            IPricingService pricingService,
            IOptions<AppSettings> settings)
        {
            _pricingService = pricingService;
            _settings = settings;
        }
       

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] List<InventoryModel> values)
        {
            long totalPrice = 0;
            var totalLoyaltyPoints = 0;

            var builder = new StringBuilder();
            builder.AppendLine("     Your Rental Invoice     ");
            builder.AppendLine();
            builder.AppendLine("Rent details:");

            foreach (var value in values)
            {
                var price = _pricingService.GetPrice(value);
                builder.AppendLine($"{value.Name}: {price} {_settings.Value.Currency}");
                totalPrice += price;
                totalLoyaltyPoints += GetLoyaltyPoint(value.Type);
            }

            builder.AppendLine();
            builder.AppendLine($"Total price: {totalPrice}{_settings.Value.Currency}");
            builder.AppendLine();
            builder.AppendLine($"Number of bonus points earned: {totalLoyaltyPoints}");

            // convert string to stream
            var byteArray = Encoding.ASCII.GetBytes(builder.ToString());
            return File(byteArray, "application/txt", "invoice.txt");
        }
        private int GetLoyaltyPoint(InventoryType type)
        {
            switch (type)
            {
                case InventoryType.Heavy:
                    return _settings.Value.HeavyLoyaltyPoint;
                case InventoryType.Regular:
                    return _settings.Value.DefaultLoyaltyPoint;
                case InventoryType.Specialized:
                    return _settings.Value.DefaultLoyaltyPoint;
                default: return 0;
            }
        }
    }
}
