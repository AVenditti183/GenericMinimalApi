using IntroMinimalApi.Models;

namespace IntroMinimalApi.Validators;

public class BlexinerValidator : AbstractValidator<Blexiner>
{
    public BlexinerValidator()
    {
        RuleFor(p => p.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(p => p.LastName).NotEmpty().MaximumLength(30);
    }
}
