namespace cosmetic_web.Models
{
    public class BasketItem
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }
        public Category Product { get; set; }
        public uint Count { get; set; }

        public bool Sold { get; set; }
    }
}