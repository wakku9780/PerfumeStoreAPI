﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStoreAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        // Navigation Property
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
