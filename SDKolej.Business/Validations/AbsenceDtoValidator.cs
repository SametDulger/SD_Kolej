using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class AbsenceDtoValidator : AbstractValidator<AbsenceDto>
    {
        public AbsenceDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Geçerli bir öğrenci seçiniz");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Geçerli bir ders seçiniz");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarih alanı boş olamaz")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Geçmiş bir tarih seçiniz");

            RuleFor(x => x.Reason)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Reason))
                .WithMessage("Sebep en fazla 500 karakter olabilir");
        }
    }
} 