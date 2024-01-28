using FluentValidation;
using TestAPI.ViewModels;

namespace TestAPI.Validators;

public class ClientCreateValidator : AbstractValidator<ClientCreateInfo>
{
    public ClientCreateValidator()
    {
        RuleFor(c => c.Inn)
            .Must(c => c.All(Char.IsDigit))
            .MinimumLength(10)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty()
            .Must(c => c.Length != 11)
            .WithMessage("ИНН введен некорректно ");
        RuleFor(c => c.Name)
            .MaximumLength(255)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя введено некорректно");

    }
}