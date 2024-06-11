using FluentValidation;

namespace ERPServer.Application.Features.Products.UpdateProduct
{
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(f => f.Name).MinimumLength(3);
            RuleFor(f => f.TypeValue).GreaterThan(0);
        }
    }
}
