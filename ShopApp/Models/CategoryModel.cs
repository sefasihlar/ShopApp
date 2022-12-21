using ShopApp.Entites;
using System.Reflection.Metadata.Ecma335;

namespace ShopApp.WebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
