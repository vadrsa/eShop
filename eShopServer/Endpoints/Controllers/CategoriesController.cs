using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Products;
using Endpoints.Controllers;
using Endpoints.DEV;
using EntityDTO.Products;
using Facades.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.Enums;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [LagResponse]
    [ApiController]
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            return await ServiceProvider.GetService<ICategoryManager>().GetAllAsync();
        }

        // GET: api/Category
        [HttpGet]
        [ResponseCache(Duration = 50)]
        [Route("tree")]
        [LagResponse]
        public async Task<IEnumerable<CategoryTreeItemDTO>> GetTree()
        {
            return await ServiceProvider.GetService<ICategoryManager>().GetTreeAsync();
        }
        
    }
}
