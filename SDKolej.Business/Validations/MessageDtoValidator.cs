using FluentValidation;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Validations
{
    public class MessageDtoValidator : AbstractValidator<MessageDto>
    {
        public MessageDtoValidator()
        {
            RuleFor(x => x.SenderId)
                .GreaterThan(0).WithMessage("Geçerli bir gönderen seçiniz");

            RuleFor(x => x.SenderRole)
                .NotEmpty().WithMessage("Gönderen rolü boş olamaz")
                .Must(role => new[] { "Öğretmen", "Veli", "Yönetici" }.Contains(role))
                .WithMessage("Geçerli bir gönderen rolü seçiniz");

            RuleFor(x => x.ReceiverId)
                .GreaterThan(0).WithMessage("Geçerli bir alıcı seçiniz");

            RuleFor(x => x.ReceiverRole)
                .NotEmpty().WithMessage("Alıcı rolü boş olamaz")
                .Must(role => new[] { "Öğretmen", "Veli", "Yönetici" }.Contains(role))
                .WithMessage("Geçerli bir alıcı rolü seçiniz");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj içeriği boş olamaz")
                .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir");
        }
    }
} 