using FluentValidation;
namespace MarketManager.Application.UseCases.Clients.Commands.CreateClient;
public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(client => client.TotalPrice).GreaterThan(0).WithMessage("TotalPrice must be greater than 0.");
        RuleFor(client => client.Discount).InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");
    }
}
