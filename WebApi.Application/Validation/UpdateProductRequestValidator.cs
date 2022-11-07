using FluentValidation;
using WebApi.Application.Dto.Product.Request;

namespace WebApi.Application.Validation
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
