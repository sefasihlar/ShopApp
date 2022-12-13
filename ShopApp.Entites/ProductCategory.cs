using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Entites
{
    public class ProductCategory
    {
        //janction yapı
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
