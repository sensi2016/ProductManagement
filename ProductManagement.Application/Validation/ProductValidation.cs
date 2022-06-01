
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Validation
{
    public class ProductValidation : AbstractValidator<RequestProductDto>
    {
        public ProductValidation(IStringLocalizer<SharedResources> stringLocalizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(stringLocalizer["Required.Title"]);
            RuleFor(x => x.Price).NotEmpty().WithMessage(stringLocalizer["Required.Price"]);
        }
    }
}
