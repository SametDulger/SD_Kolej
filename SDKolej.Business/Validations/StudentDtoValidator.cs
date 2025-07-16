using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.SchoolNumber)
                .NotEmpty().WithMessage("Okul numarası boş olamaz")
                .Matches(@"^\d+-\d{2}$").WithMessage("Okul numarası formatı: 100-25 şeklinde olmalıdır");

            RuleFor(x => x.GraduationGPA)
                .InclusiveBetween(0, 100).When(x => x.GraduationGPA.HasValue)
                .WithMessage("Mezuniyet ortalaması 0-100 arasında olmalıdır");
        }
    }
} 