using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class AnnouncementDtoValidator : AbstractValidator<AnnouncementDto>
    {
        public AnnouncementDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş olamaz")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik alanı boş olamaz")
                .MaximumLength(2000).WithMessage("İçerik en fazla 2000 karakter olabilir");

            RuleFor(x => x.PublishDate)
                .NotEmpty().WithMessage("Yayın tarihi boş olamaz")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Geçmiş bir tarih seçiniz");
        }
    }
} 