using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApp.Entites;

namespace ShopApp.WebUI.Models
{
   
    public class CategoryListViewModel
    {
        public string SelectedCategory { get; set; }
        public List<Category> kategoriler { get; set; }
    }
}
