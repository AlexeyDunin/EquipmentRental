using System;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public InventoryType Type { get; set; }
    }
}