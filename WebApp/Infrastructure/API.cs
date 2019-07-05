using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public static class Api
    {
        public static class Inventory
        {
            public static string GetAllCatalogItems(string apiUrl) => $"{apiUrl}/inventory";
        }

        public static class Basket
        {
            public static string GetAllBaskets(string apiUrl) => $"{apiUrl}/basket";
        }
    }
}
