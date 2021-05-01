using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductDirector director = new ProductDirector(); //ürünümüzü üretmesi için çağırdık
            var builder = new NewCustomerProductBuilder(); //new veya old müşterilerimizin hangisini çağırmak istersek onu yazdık
            director.GenerateProduct(builder);
            var model = builder.GetModel(); //modelimizi çağırdık

            Console.WriteLine(model.Id); //burada yazdığımız Builder desenini çağırarak modelimizin özelliklerini ve indirim işlemlerini çağırdık
            Console.WriteLine(model.CategoryName);
            Console.WriteLine(model.DiscountApplied);
            Console.WriteLine(model.DiscountedPrice);
            Console.WriteLine(model.ProductName);
            Console.WriteLine(model.UnitPrice);

            Console.ReadLine();
        }
    }

    class ProductViewModel //öncelikle builder'in en önemli katmanı nesnedir yani nesne üzerine çalışmaktır bu yüzden bir class kuruyoruz
    {
        public int Id { get; set; } //nesnenin özelliklerini yazıyoruz
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountedPrice { get; set; } //discount = indirim
        public bool DiscountApplied { get; set; }
    }

    abstract class ProductBuilder //bu nesnenin bir business katmanına ihtiyacı var şimdi onu yazıyoruz
    {
        public abstract void GetProductData(); //nesnenin verileriyle alakalı
        public abstract void ApplyDiscount(); //indirimle alakalı
        public abstract ProductViewModel GetModel(); //modelin döndürülmesiyle alakalı
    }

    class NewCustomerProductBuilder : ProductBuilder //yeni müşteriler için oluşturmak istersek
    {
        ProductViewModel model = new ProductViewModel();
        public override void ApplyDiscount()
        {
            model.DiscountedPrice = model.UnitPrice*(decimal) 0.90; //indirim uygulansın mı diye yazdık
            model.DiscountApplied = true;
        }

        public override ProductViewModel GetModel()
        {
            return model; //ürettiğin modeli döndürme işlemi
        }

        public override void GetProductData()
        {
            model.Id = 1;                        //ürün bilgilerini yazdık
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }
    }

    class OldCustomerProductBuilder : ProductBuilder //eski müşteriler için oluşturmak istersek
    {
        ProductViewModel model = new ProductViewModel();
        public override void ApplyDiscount()
        {
            model.DiscountedPrice = model.UnitPrice;
            model.DiscountApplied = false;
        }

        public override ProductViewModel GetModel()
        {
            return model;
        }

        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }
    }

    //artık ihtiyacımız olan tek şey bu product'ı üretmek

    class ProductDirector //bununla beraber artık desen oluşmuştur şimdi ana bölüme geçebiliriz
    {
        public void GenerateProduct(ProductBuilder productBuilder)
        {
            productBuilder.GetProductData();
            productBuilder.ApplyDiscount();
        }
    }
}
