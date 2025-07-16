using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class DocumentDtoValidator : AbstractValidator<DocumentDto>
    {
        public DocumentDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Geçerli bir öğrenci seçiniz");

            RuleFor(x => x.DocumentType)
                .NotEmpty().WithMessage("Belge türü boş olamaz")
                .Must(type => new[] { "Karne", "Teşekkür", "Takdir", "Onur" }.Contains(type))
                .WithMessage("Geçerli bir belge türü seçiniz");

            RuleFor(x => x.FilePath)
                .NotEmpty().WithMessage("Dosya yolu boş olamaz")
                .MaximumLength(500).WithMessage("Dosya yolu en fazla 500 karakter olabilir");
        }
    }
} 