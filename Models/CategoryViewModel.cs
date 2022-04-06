using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace cosmetic_web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

       
        [Range(1, 1000, ErrorMessage = "Price cannot be more than 1000!")]
        public uint Price { get; set; }
        public string? ImagePath { get; set; }

        public IFormFile? Image { get; set; }
        public Category CreateModel()
        {
            return new Category
            {
                Id = Id,
                Name = Name,
                Price = Price,
                ImagePath = ImagePath
            };
        }
    }
}
