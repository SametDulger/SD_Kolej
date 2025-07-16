using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class EnrollmentDtoValidator : AbstractValidator<EnrollmentDto>
    {
        public EnrollmentDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Geçerli bir öğrenci seçiniz");

            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("Geçerli bir sınıf seçiniz");

            RuleFor(x => x.Year)
                .InclusiveBetween(2020, 2030).WithMessage("Geçerli bir yıl seçiniz");
        }
    }
} 