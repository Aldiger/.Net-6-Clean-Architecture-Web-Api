using FluentValidation;
using WebApi.Application.Dto.Product.Request;

namespace WebApi.Application.Validation
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
