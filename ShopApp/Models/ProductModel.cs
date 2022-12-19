using ShopApp.Entites;

namespace ShopApp.WebUI.Models
{
    public class PageInfo
    {
        //page özellikleri
        public int TotalItems { get; set; }
        //her sayfada jaç eleman gösterilecek
        public int ItemsPerPage { get; set; }
        //hangi sayfadayiz
        public int CurrentPage { get; set; }
        //aktif olan category bilgisi
        public string CurrenCategory { get; set; }

        //toplam ürünün kac sayfada göstermek istiyorsak bu işlemi kullanıyoruz
        public int TotalPages()
        {
            //math.celling yuvarlama işlemi yapıyor
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
    public class ProductModel
    {
        //sayfa bilgisi
        public PageInfo PageInfo { get; set; }

        public List<Product> Products { get; set; }
    }
}
