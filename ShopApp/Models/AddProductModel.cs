using Microsoft.Build.Graph;
using Newtonsoft.Json.Serialization;
using ShopApp.Entites;
using System.ComponentModel.DataAnnotations;


namespace ShopApp.WebUI.Models
{
    public class AddProductModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60,MinimumLength =6,ErrorMessage = "Must be a minimum of 6 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Image extension is not correct")]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Enter price round")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string? Condition { get; set; }


        public List<Category> Categories { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
