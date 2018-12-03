using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities.Products;
using BusinessEntities.Serialization;
using Endpoints.DEV;
using EntityDTO.Products;
using Facades.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedEntities.Enums;
using System;

namespace Endpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LagResponse]
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IServiceProvider serviceProvider) : base(serviceProvider)

        { }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<ProductInfoDTO>> GetAll()
        {
            return await ServiceProvider.GetService<IProductManager>().GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetProductByID")]
        public async Task<ProductDetailDTO> GetByID(int id)
        {
            return await ServiceProvider.GetService<IProductManager>().FindByIDAsync(id);
        }

        [HttpGet("OrderCriterias", Name = "GetOrderByCriterias")]
        public List<EnumItemSerialized> GetOrderByCriterias()
        {
            throw new NotImplementedException();
        }

        [HttpGet("Search", Name = "Search")]
        public async Task<List<Product>> Search(string query, int categoryID, int limit = 30, int start = 0, ProductOrderBy orderBy = ProductOrderBy.Name, bool ascending = true)
        {
            throw new NotImplementedException();

        }

        [HttpGet("SearchCount", Name = "SearchCount")]
        public int SearchCount(string query, int categoryID)
        {
            throw new NotImplementedException();

        }

        [HttpPost]
        public async Task<ProductDetailDTO> Add(ProductDetailDTO item)
        {
            return await ServiceProvider.GetService<IProductManager>().InsertAsync(item);

        }

        [HttpPut]
        public async Task<ProductDetailDTO> Update(ProductDetailDTO item)
        {
            return await ServiceProvider.GetService<IProductManager>().UpdateAsync(item);

        }

        [HttpDelete]
        public async Task<bool> Delete(ProductInfoDTO obj)
        {
            await ServiceProvider.GetService<IProductManager>().RemoveAsync(obj);
            return true;
        }


    }
}
