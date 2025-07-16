using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class BranchDtoValidator : AbstractValidator<BranchDto>
    {
        public BranchDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şube adı boş olamaz")
                .MaximumLength(100).WithMessage("Şube adı en fazla 100 karakter olabilir");
        }
    }
} 