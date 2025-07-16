using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class GradeDtoValidator : AbstractValidator<GradeDto>
    {
        public GradeDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Geçerli bir öğrenci seçiniz");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Geçerli bir ders seçiniz");

            RuleFor(x => x.Term)
                .InclusiveBetween(1, 2).WithMessage("Dönem 1 veya 2 olmalıdır");

            RuleFor(x => x.Value)
                .InclusiveBetween(0, 100).WithMessage("Not 0-100 arasında olmalıdır");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Açıklama en fazla 500 karakter olabilir");
        }
    }
} 