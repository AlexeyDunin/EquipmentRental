using System.Collections.Generic;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IInventoryRepository
    {
        List<InventoryModel> Inventory { get; }
        InventoryModel GetInventoryById(int id);
    }
}