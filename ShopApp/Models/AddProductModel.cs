﻿namespace ShopApp.WebUI.Models
{
    public class AddProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string? Gender { get; set; }
        public string? Condition { get; set; }
    }
}
