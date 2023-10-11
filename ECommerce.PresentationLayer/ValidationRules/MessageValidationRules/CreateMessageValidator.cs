using ECommerce.DtoLayer.Dtos.Messages;
using FluentValidation;
using System.Globalization;

namespace ECommerce.PresentationLayer.ValidationRules.MessageValidationRules
{
    public class CreateMessageValidator :AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidator()
        {

            RuleFor(x => x.ReceiverUserName).NotEmpty().WithMessage("Kime alanı boş geçilemez")
                .MaximumLength(50).WithMessage("Kime alanı 50 karakterden fazla olamaz");

            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanı boş geçilemez.")
            .MinimumLength(2).WithMessage("Konu alanı minimim 2 karakterden oluşabilir.")
            .MaximumLength(50).WithMessage("Konu alanı maksimum 50 karakterden oluşabilir.");

            RuleFor(x => x.Body).MaximumLength(800).WithMessage("Mesaj alanı maksimum 800 karakterden oluşabilir.")
            .NotEmpty().WithMessage("Mesaj alanı boş geçilemez.")
            .MinimumLength(2).WithMessage("Mesaj alanı minimum 2 karakterden oluşabilir");

       
        }
    }
}
