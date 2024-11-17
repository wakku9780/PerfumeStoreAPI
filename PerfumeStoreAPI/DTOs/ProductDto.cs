namespace PerfumeStoreAPI.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string FragranceType { get; set; }
        public DateTime LaunchDate { get; set; }
        public float Rating { get; set; }
    }
}
