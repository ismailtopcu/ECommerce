using ECommerce.DtoLayer.Dtos.Product;
using FluentValidation;

namespace ECommerce.PresentationLayer.ValidationRules.ProductValidationRules
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez")
                .MinimumLength(2).WithMessage("İsim alanı minimum 2 karakter olabilir")
                .MaximumLength(100).WithMessage("İsim alanı maksimum 100 karakter olabilir");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stok alanı boş geçilemez");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez");

            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama alanı maksimum 1000 karakter olabilir");

        }
    }
}
