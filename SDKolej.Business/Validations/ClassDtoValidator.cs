using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class ClassDtoValidator : AbstractValidator<ClassDto>
    {
        public ClassDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sınıf adı boş olamaz")
                .MaximumLength(50).WithMessage("Sınıf adı en fazla 50 karakter olabilir");

            RuleFor(x => x.BranchId)
                .GreaterThan(0).When(x => x.BranchId.HasValue)
                .WithMessage("Geçerli bir şube seçiniz");
        }
    }
} 