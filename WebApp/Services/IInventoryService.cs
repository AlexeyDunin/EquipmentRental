using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public interface IInventoryService
    {
        List<InventoryModel> GetItems();
    }
}