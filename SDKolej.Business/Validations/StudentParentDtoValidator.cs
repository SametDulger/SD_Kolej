using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class StudentParentDtoValidator : AbstractValidator<StudentParentDto>
    {
        public StudentParentDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Geçerli bir öğrenci seçiniz");

            RuleFor(x => x.ParentId)
                .GreaterThan(0).WithMessage("Geçerli bir veli seçiniz");
        }
    }
} 