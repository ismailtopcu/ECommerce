using ECommerce.DtoLayer.Dtos.Category;

namespace ECommerce.DtoLayer.Dtos.Product
{
    public record ResultProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }        
        public ResultCategoryDto Category { get; set; }
    };

}
