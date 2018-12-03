using EntityDTO.Products;
using eShopApi.Validation.DTO;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace eShopApi.Validation
{
    internal static class ValidatorConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IValidator<ProductDetailDTO>, ProductDetailDTOValidator>();
        }
    }
}
