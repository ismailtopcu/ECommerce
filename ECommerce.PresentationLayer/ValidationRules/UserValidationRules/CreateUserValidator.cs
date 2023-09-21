using ECommerce.DtoLayer.Dtos.AccountDto;
using FluentValidation;

namespace ECommerce.PresentationLayer.ValidationRules.UserValidationRules
{
	public class CreateUserValidator : AbstractValidator<CreateNewUserDto>
	{
        public CreateUserValidator()
        {
			RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez")
				.MinimumLength(3).WithMessage("Lütfen en az 3 karakter veri girişi yapınız")
				.MaximumLength(20).WithMessage("İsim çok uzun!");

			RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim Alanı Boş Geçilemez")
				.WithMessage("Lütfen en az 2 karakter veri girişi yapınız")
				.MaximumLength(20).WithMessage("Soyisim çok uzun!");

			RuleFor(x => x.City).NotEmpty().WithMessage("Şehir Alanı Boş Geçilemez");
			RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Geçilemez")
				.MinimumLength(3).WithMessage("Lütfen en az 3 karakter veri girişi yapınız")
				.MaximumLength(3).WithMessage("Kullanıcı Adı çok uzun!");


			RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail Adresi Boş Geçilemez").EmailAddress().WithMessage("Geçerli bir E-Posta adresi girin"); 
			RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Bilgisi Boş Geçilemez");
			RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor");


			
		}
    }
}
