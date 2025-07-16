using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class FileUploadDtoValidator : AbstractValidator<FileUploadDto>
    {
        public FileUploadDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Geçerli bir öğrenci seçiniz");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("Dosya adı boş olamaz")
                .MaximumLength(255).WithMessage("Dosya adı en fazla 255 karakter olabilir");

            RuleFor(x => x.FilePath)
                .NotEmpty().WithMessage("Dosya yolu boş olamaz")
                .MaximumLength(500).WithMessage("Dosya yolu en fazla 500 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Açıklama en fazla 1000 karakter olabilir");
        }
    }
} 