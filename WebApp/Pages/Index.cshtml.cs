using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryRepository _inventoryRepository;

        public IndexModel(IInventoryRepository inventoryRepository) => _inventoryRepository = inventoryRepository;

        public List<InventoryModel> Equipments { get; set; }
        public string  Message { get; set; }

        public void OnGet()
        {
            Equipments = _inventoryRepository.Inventory;
        }

        public IActionResult OnGetPartial() =>
            Partial("_Basket");
    }
}
