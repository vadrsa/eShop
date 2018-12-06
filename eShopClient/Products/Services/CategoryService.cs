using EntityDTO.Products;
using eShopUI.Infrastructure.Api;
using Flurl.Http;
using ModelChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Products.Services
{
    class CategoryService : RestConsumingService<CategoryDTO>
    {
        public CategoryService() : base("categories")
        {

        }

        public async Task<CategoryTreeItemDTO> GetCategoriesTree(CancellationToken token = new CancellationToken())
        {
            var response = (await (ControllerPath + "tree").GetJsonAsync(token)).ConfigureAwait(false);
            return response;
        }

        public async Task<bool> SaveTrackableList(ITrackableCollection<CategoryDTO> collection, CancellationToken token = new CancellationToken())
        {
            try
            {
                var response = (await (ControllerPath + "editable").PostJsonAsync(collection, token).ReceiveJson<bool>());
                return response;

            }
            catch (Exception e)
            {

                throw e;
            }
        }



    }
}
