using System.Collections.Generic;
using Pricing.Models;

namespace Pricing
{
    public class AppSettings
    {
        public List<MarketingStrategy> MarketingStrategy { get; set; }
        public int OneTimeFee { get; set; }
        public int PremiumDailyFee { get; set; }
        public int RegularDailyFee { get; set; }
        public int HeavyLoyaltyPoint { get; set; }
        public int DefaultLoyaltyPoint { get; set; }
        public string Currency { get; set; }
    }

    public class MarketingStrategy
    {
        public InventoryType InventoryType { get; set; }
        public int PremiumDays { get; set; }
        public bool OneTimeFee { get; set; }
    }
}