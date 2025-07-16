using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class TeacherCourseDtoValidator : AbstractValidator<TeacherCourseDto>
    {
        public TeacherCourseDtoValidator()
        {
            RuleFor(x => x.TeacherId)
                .GreaterThan(0).WithMessage("Geçerli bir öğretmen seçiniz");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Geçerli bir ders seçiniz");
        }
    }
} 