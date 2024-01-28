using FluentValidation;
using TestAPI.ViewModels;

namespace TestAPI.Validators;

public class FounderUpdateValidator : AbstractValidator<FounderUpdate>
{
    public FounderUpdateValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("ID не заполнено");
        RuleFor(f => f.Fio)
            .MaximumLength(255)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя введено некорректно");
    }
}