using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Products;
using BusinessEntities.Serialization;
using EntityDTO.Products;
using eShopApi.BusinessLogic.Global;
using eShopApi.BusinessLogic.Products;
using eShopApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedEntities.Enums;

namespace Endpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LagResponse]
    public class ProductsController : ControllerBase
    {
        #region Private Fields
        #endregion


        public ProductsController()
        {
        }


        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<ProductInfoDTO>> Get()
        {
            return Mapper.Map<List<ProductInfoDTO>>(await Manager.SelectAllAsync());
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ProductDetailDTO> GetByID(int id)
        {
            return Mapper.Map<ProductDetailDTO>(await Manager.SelectByKeyAsync(id));
        }

        [HttpGet("OrderCriterias", Name = "GetOrderByCriterias")]
        public List<EnumItemSerialized> GetOrderByCriterias()
        {
            return EnumSerializationHelper.Serialize(typeof(ProductOrderBy));
        }
        
        [HttpGet("Search", Name = "Search")]
        public async Task<List<Product>> Search(string query, int categoryID, int limit = 30, int start = 0, ProductOrderBy orderBy = ProductOrderBy.Name, bool ascending = true)
        {
            return (await Manager.SelectAllAsync()).Take(categoryID).ToList();
        }

        [HttpGet("SearchCount", Name = "SearchCount")]
        public int SearchCount(string query, int categoryID)
        {
            return categoryID;
        }

        [HttpPost]
        public async Task<ProductDetailDTO> Add(ProductDetailDTO item)
        {
            Product toInsert = Mapper.Map<ProductDetailDTO, Product>(item);
            int id = (await Manager.InsertWithImageAsync(toInsert, item.ImageBytes)).ID;
            return await GetByID(id);
        }

        [HttpPut]
        public void Update(ProductDetailDTO item)
        {
            
            Manager.UpdateAsync(Mapper.Map<ProductDetailDTO, Product>(item));
        }

        [HttpDelete]
        public async Task<bool> Delete(ProductInfoDTO obj)
        {
            return await Manager.Remove(obj.ID);
        }


    }
}
