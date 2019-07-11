namespace WebApp.ViewModels
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public InventoryType Type { get; set; }
        public int RentalDays { get; set; } 
    }
}