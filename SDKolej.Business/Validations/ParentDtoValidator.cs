using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class ParentDtoValidator : AbstractValidator<ParentDto>
    {
        public ParentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon alanı boş olamaz")
                .Matches(@"^[0-9]{10,11}$").WithMessage("Geçerli bir telefon numarası giriniz");
        }
    }
} 