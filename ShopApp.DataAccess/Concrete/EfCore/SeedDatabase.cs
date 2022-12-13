using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void seed()
        {
            var _context = new ShopContext();

            if (_context.Database.GetPendingMigrations().Count() == 0)
            {
                if (_context.Categories.Count() == 0)
                {
                    _context.Categories.AddRange(Categories);
                }

                if (_context.Products.Count() == 0)
                {
                    _context.Products.AddRange(Products);
                }

                _context.SaveChanges();


            }

        }

        private static Category[] Categories =
        {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"}
        };

        private static Product[] Products =
        {
            new Product() {Name="IPhone 4", Price=1000,ImageUrl="1.jpg"},
            new Product() {Name="IPhone 5", Price=44000,ImageUrl="2.jpg"},
            new Product() {Name="IPhone 6", Price=1000,ImageUrl="3.jpg"},
            new Product() {Name="IPhone 7", Price=20000,ImageUrl="4.jpg"},
            new Product() {Name="IPhone 8", Price=7000,ImageUrl="5.jpg"},
            new Product() {Name="IPhone X", Price=9000,ImageUrl="6.jpg"},
            new Product() {Name="IPhone 11", Price=8000,ImageUrl="7.jpg"},

            
        };

    }
}
