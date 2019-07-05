using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IInventoryService _inventoryService;
        private List<InventoryModel> _inventory;

        public InventoryRepository(IInventoryService inventoryService) => _inventoryService = inventoryService;

        public List<InventoryModel> Inventory => _inventory ?? Setup();

        private List<InventoryModel> Setup()
        {
            _inventory = _inventoryService.GetItems();
            return _inventory;
        }

        public InventoryModel GetInventoryById(int id)
        {
            return Inventory.Find(_ => _.Id.Equals(id));
        }
    }
}
