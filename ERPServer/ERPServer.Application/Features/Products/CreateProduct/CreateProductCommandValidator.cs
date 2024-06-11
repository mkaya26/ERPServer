using FluentValidation;

namespace ERPServer.Application.Features.Products.CreateProduct
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(f => f.Name).MinimumLength(3);
            RuleFor(f => f.TypeValue).GreaterThan(0);
        }
    }
}
