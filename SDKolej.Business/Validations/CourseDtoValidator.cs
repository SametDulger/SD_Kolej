using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ders adı boş olamaz")
                .MaximumLength(100).WithMessage("Ders adı en fazla 100 karakter olabilir");
        }
    }
} 