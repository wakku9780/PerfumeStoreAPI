using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PerfumeStoreAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string Brand { get; set; }

        [MaxLength(10)]
        public string Size { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        [MaxLength(50)]
        public string FragranceType { get; set; }

        public DateTime LaunchDate { get; set; }

        public float Rating { get; set; }

        // Navigation Property
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
