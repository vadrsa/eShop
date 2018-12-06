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
using ModelChangeTracking;
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

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            return await ServiceProvider.GetService<ICategoryManager>().GetAllAsync();
        }

        // GET: api/Categories
        [HttpGet]
        [ResponseCache(Duration = 50)]
        [Route("tree")]
        [LagResponse]
        public async Task<IEnumerable<CategoryTreeItemDTO>> GetTree()
        {
            return await ServiceProvider.GetService<ICategoryManager>().GetTreeAsync();
        }

        // Post: api/Categories/editable
        [HttpPost]
        [Route("editable")]
        [LagResponse]
        public async Task<bool> SaveEditableList(TrackableContainer<CategoryDTO> trackableCollection = null)
        {
            return await ServiceProvider.GetService<ICategoryManager>().SaveEditableListAsync(trackableCollection);
        }

    }
}
