using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Bars;
using BusinessEntities.Products;
using EntityDTO.Products;
using eShopApi.BusinessLogic.Products;
using eShopApi.ResourceAccess.Bars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.Enums;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [LagResponse]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private BrandManager Manager { get; set; }

        public BrandsController(BrandManager manager)
        {
            Manager = manager;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<IEnumerable<BrandInfoDTO>> Get()
        {
            return Mapper.Map<IEnumerable<BrandInfoDTO>>(await Manager.SelectAllAsync());
        }

        //// GET: api/Brands/5
        //[HttpGet("{id}", Name = "GetBrand")]
        //public Brand Get(int id)
        //{
        //    return Manager.SelectByKey(id);
        //}

        //[HttpGet]
        //[Route("baritems")]
        //[ResponseCache(Duration = 50)]
        //public IEnumerable<Brand> GetBrandBarItems()
        //{
        //    return Manager.GetBrandBar();
        //}


        //[HttpGet("{id}/products", Name = "GetBrandProducts")]
        //public IEnumerable<Product> GetBrandProducts(int id, int limit = 30, int start = 0, ProductOrderBy orderBy = ProductOrderBy.Name, bool ascending = true)
        //{
        //    return Manager.GetProducts(id, limit, start, orderBy, ascending);
        //}

        //[HttpGet("{id}/productsCount", Name = "GetBrandProductsCount")]
        //public int GetBrandProductsCount(int id)
        //{
        //    return Manager.GetProductsCount(id);
        //}
    }
}
