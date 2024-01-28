using FluentValidation;
using TestAPI.ViewModels;

namespace TestAPI.Validators;

public class FounderCreateValidator : AbstractValidator<FounderCreateInfo>
{
    public FounderCreateValidator()
    {
        RuleFor(f => f.Inn)
            .Must(c => c.All(Char.IsDigit))
            .MinimumLength(10)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty()
            .Must(c => c.Length != 11)
            .WithMessage("ИНН введен некорректно ");
        RuleFor(f => f.Fio)
            .MaximumLength(255)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя введено некорректно");

    }
}