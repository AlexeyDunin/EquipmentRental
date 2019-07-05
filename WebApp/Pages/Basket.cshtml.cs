using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.ViewModels;

namespace WebApp.Pages
{
    public class BasketModel : PageModel
    {
        public void OnPost([FromBody] ItemModel item)
        {
            var test = item.Id;
        }
    }
  
}