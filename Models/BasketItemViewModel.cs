namespace cosmetic_web.Models
{
    public class BasketItemViewModel
    {
   
        public int ProductId { get; set; }

        public string Name { get; set; }

        public uint Price { get; set; }

        public uint Count { get; set; }

        public string? ImagePath { get; set; }

        public bool Sold { get; set; }
    }
}
