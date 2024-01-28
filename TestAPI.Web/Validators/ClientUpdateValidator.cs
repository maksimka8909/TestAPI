using FluentValidation;
using TestAPI.ViewModels;

namespace TestAPI.Validators;

public class ClientUpdateValidator : AbstractValidator<ClientUpdate>
{
    public ClientUpdateValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("ID не заполнено");
        RuleFor(c => c.Name)
            .MaximumLength(255)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя введено некорректно");
    }
}