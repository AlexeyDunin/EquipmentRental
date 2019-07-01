using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public IndexModel(IInventoryService inventoryService) => _inventoryService = inventoryService;

        public List<InventoryModel> Equipments { get; set; }
        public string  Message { get; set; }

        public void OnGet()
        {
            Equipments = _inventoryService.GetItems();
        }

        public IActionResult OnPost(InventoryModel inventoryItemViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Message = "Ok";
            return Page();
        }
    }
}
