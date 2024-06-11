using FluentValidation;

namespace ERPServer.Application.Features.Depots.CreateDepot
{
    public sealed class CreateDepotCommandValidator : AbstractValidator<CreateDepotCommand>
    {
        public CreateDepotCommandValidator()
        {
            RuleFor(f => f.Name).MinimumLength(3);
            RuleFor(f => f.City).MinimumLength(3);
            RuleFor(f => f.Town).MinimumLength(3);
            RuleFor(f => f.FullAddress).MinimumLength(3).MaximumLength(250);
        }
    }
}
