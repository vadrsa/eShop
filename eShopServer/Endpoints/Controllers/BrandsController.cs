using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Bars;
using BusinessEntities.Products;
using Endpoints.Controllers;
using Endpoints.DEV;
using EntityDTO.Products;
using Facades.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [LagResponse]
    [ApiController]
    public class BrandsController : ApiControllerBase
    {
        public BrandsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        // GET: api/Brands
        [HttpGet]
        public async Task<IEnumerable<BrandInfoDTO>> Get()
        {
            return await ServiceProvider.GetService<IBrandManager>().GetAllAsync();
        }
        
        [HttpGet("{id}", Name ="GetBrandByID")]
        public async Task<BrandDetailDTO> GetByID(int id)
        {
            return await ServiceProvider.GetService<IBrandManager>().FindByIDAsync(id);
        }


        [HttpPost]
        public async Task<BrandDetailDTO> Add(BrandDetailDTO item)
        {
            return await ServiceProvider.GetService<IBrandManager>().InsertAsync(item);

        }

        [HttpPut]
        public async Task<BrandDetailDTO> Update(BrandDetailDTO item)
        {
            return await ServiceProvider.GetService<IBrandManager>().UpdateAsync(item);

        }

        [HttpDelete]
        public async Task<bool> Delete(BrandInfoDTO obj)
        {
            await ServiceProvider.GetService<IBrandManager>().RemoveAsync(obj);
            return true;
        }

    }
}
