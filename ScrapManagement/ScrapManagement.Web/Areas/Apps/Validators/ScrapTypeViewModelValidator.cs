using FluentValidation;
using ScrapManagement.Web.Areas.Apps.Models;

namespace ScrapManagement.Web.Areas.Apps.Validators
{
    public class ScrapTypeViewModelValidator : AbstractValidator<ScrapTypeViewModel>
    {
        public ScrapTypeViewModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} alanı zorunludur.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} alanı 50 karakteri geçmemelidir.");
        }
    }
}