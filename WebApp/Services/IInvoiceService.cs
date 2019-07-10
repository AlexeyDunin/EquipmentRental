using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public interface IInvoiceService
    {
        Task<HttpContent> GetInvoice(List<InventoryModel> inventoryModels);
    }
}