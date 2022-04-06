using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace cosmetic_web.Models
{
    public class Category
    {   
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public uint Price { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string? ImagePath { get; set; }
    }
}
