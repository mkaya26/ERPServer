using FluentValidation;

namespace ERPServer.Application.Features.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(f => f.Name).MinimumLength(3);
            RuleFor(f => f.TaxDepartment).MinimumLength(3);
            RuleFor(f => f.TaxNumber).MinimumLength(10).MaximumLength(11);
            RuleFor(f => f.City).MinimumLength(3);
            RuleFor(f => f.Town).MinimumLength(3);
            RuleFor(f => f.FullAddress).MinimumLength(3).MaximumLength(250);
        }
    }
}