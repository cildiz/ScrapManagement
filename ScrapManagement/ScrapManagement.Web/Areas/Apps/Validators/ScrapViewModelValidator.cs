using FluentValidation;
using ScrapManagement.Web.Areas.Apps.Models;

namespace ScrapManagement.Web.Areas.Apps.Validators
{
    public class ScrapViewModelValidator : AbstractValidator<ScrapViewModel>
    {
        public ScrapViewModelValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("{PropertyName} alanı zorunludur.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} alanı 50 karakteri geçmemelidir.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} alanı zorunludur.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} alanı 50 karakteri geçmemelidir.");
        }
    }
}