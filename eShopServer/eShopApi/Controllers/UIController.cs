using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntities;
using eShopApi.ResourceAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LagResponse]

    public class UIController : ControllerBase
    {
        // GET: api/UI/HomeSlider
        //[HttpGet("HomeSlider")]
        //public IEnumerable<HomeSliderItem> GetHomeSlider()
        //{
        //    return HomeSliderRepository.Instance.SelectAll();
        //}

    }
}
