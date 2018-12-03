using EntityDTO.Products;
using eShopApi.Options;
using eShopApi.Validation.CustomValidators;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApi.Validation.DTO
{
    internal class ProductDetailDTOValidator : AbstractValidator<ProductDetailDTO>
    {
        public ProductDetailDTOValidator(IOptions<GlobalOptions> global)
        {

            RuleFor(x => 
            x.Name).NotEmpty().MinimumLength(3);

            RuleFor(x => x.CategoryID).GreaterThan(0);

            RuleFor(x => x.BrandID).GreaterThan(0);

            RuleFor(x => x.ImageBytes)
                .Must(bytes => bytes.Length <= global.Value.MaxAllowedArrayLength).MustBeImage().When(i => i.ImageBytes != null);
        }
    }
}
