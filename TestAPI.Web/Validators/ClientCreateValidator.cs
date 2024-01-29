using FluentValidation;
using TestAPI.ViewModels;

namespace TestAPI.Validators;

public class ClientCreateValidator : AbstractValidator<ClientCreateInfo>
{
    public ClientCreateValidator()
    {
        RuleFor(c => c.TaxpayerNumber)
            .Must(c => c.All(Char.IsDigit))
            .NotNull()
            .NotEmpty()
            .Must(c => c.Length is 10 or 12)
            .WithMessage("ИНН введен некорректно ");
        RuleFor(c => c.Name)
            .MaximumLength(255)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя введено некорректно");
    }
}