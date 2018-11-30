using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Api
{
    public static class ApiConfig
    {
        public static string BaseUrl
        {
            get
            {
                return "https://localhost:44308/api/";
            }
        }

        public static string ResourceBaseUrl
        {
            get
            {
                return "https://localhost:44308/";
            }
        }
    }
}
