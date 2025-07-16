using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class ClassCourseDtoValidator : AbstractValidator<ClassCourseDto>
    {
        public ClassCourseDtoValidator()
        {
            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("Geçerli bir sınıf seçiniz");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Geçerli bir ders seçiniz");
        }
    }
} 