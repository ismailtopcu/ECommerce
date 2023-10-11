using ECommerce.DtoLayer.Dtos.Messages;
using FluentValidation;

namespace ECommerce.PresentationLayer.ValidationRules.MailValidationRules
{
	public class CreateMailValidator : AbstractValidator<CreateMailDto>
	{
		public CreateMailValidator() 
		{
			RuleFor(x=>x.Email).NotEmpty().WithMessage("E-Posta adresi boş geçilemez.")
			.EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");

			RuleFor(x=>x.Subject).NotEmpty().WithMessage("Konu alanı boş geçilemez.")
			.MinimumLength(2).WithMessage("Konu alanı minimim 2 karakterden oluşabilir.")
			.MaximumLength(50).WithMessage("Konu alanı maksimum 50 karakterden oluşabilir.");

			RuleFor(x => x.Body).MaximumLength(800).WithMessage("Mesaj alanı maksimum 800 karakterden oluşabilir.")
			.NotEmpty().WithMessage("Mesaj alanı boş geçilemez.")
			.MinimumLength(10).WithMessage("Mesaj alanı minimum 10 karakterden oluşabilir");

			RuleFor(x => x.To).NotEmpty().WithMessage("Kime alanı boş geçilemez")
				.MaximumLength(70).WithMessage("Kime alanı maksimum 70 karakterden oluşabilir");
		}
	}
}
