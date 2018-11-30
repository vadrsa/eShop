using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Products;
using EntityDTO.Products;
using eShopApi.BusinessLogic.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.Enums;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [LagResponse]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoryManager Manager { get; set; }

        public CategoriesController(CategoryManager manager)
        {
            Manager = manager;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            return Mapper.Map<IEnumerable<CategoryDTO>>(await Manager.SelectAllAsync());
        }

        // GET: api/Category
        [HttpGet]
        [ResponseCache(Duration = 50)]
        [Route("tree")]
        [LagResponse]
        public async Task<IEnumerable<CategoryTreeItemDTO>> GetTree()
        {
            return await Manager.GetCategoryTree();
        }

        //// GET: api/Category/5
        //[HttpGet("{id}", Name = "GetCategory")]
        //public Category Get(int id)
        //{
        //    return Manager.SelectByKey(id);
        //}

        //[LagResponse]
        //[HttpGet("{id}/products", Name = "GetCategoryProducts")]
        //public IEnumerable<Product> GetCategoryProducts(int id, int limit = 30, int start=0, ProductOrderBy orderBy = ProductOrderBy.Name, bool ascending = true)
        //{
        //    return Manager.GetCategoryProducts(id, limit, start, orderBy, ascending);
        //}

        //[HttpGet("{id}/productsCount", Name = "GetCategoryProductsCount")]
        //public int GetCategoryProductsCount(int id)
        //{
        //    return Manager.GetCategoryProductsCount(id);
        //}
    }
}
