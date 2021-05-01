using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject; //buraya ninject'i ekleyin

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda

            //basit bir container vasıtasıyla managerımız ondan beslenmiş oldu
            IKernel kernel = new StandardKernel(); //ninject içinden
            kernel.Bind<IProductDal>().To<EfProductDal>().InSingletonScope(); //eğer bir stand IProductDal isterse EfProductDal instance'ını oluşturup ver demek

            ProductManager productManager = new ProductManager(kernel.Get<IProductDal>()); //NhProductDal ile çalışmak istersek onu yazarız
            productManager.Save();

            Console.ReadLine();
        }
    }
    //buradaki sıkıntı şu olsun; binlerce koda Ef ile çalıştığımızı yazdık farklı bir yapıyla çalışmaya geçseydik silip tekrar yazılması baya zaman alırdı bunu düzelteceğiz (DI ile)
    //bunu düzeltmek için bir interface ile çalışmamız gerekecek 
    interface IProductDal
    {
        void Save();
    }
    class EfProductDal:IProductDal //bir nesnemiz olduğunu varsayalım ve bunu artık interface'e bağlayalım
    {
        public void Save()
        {
            Console.WriteLine("Save with Ef");
        }
    }
    class NhProductDal : IProductDal //artık farklı yapılarla da çalışılabilecek bir yapı kurduk  başka yapılar da ekleyebiliriz
    { 
        public void Save()
        {
            Console.WriteLine("Save with Nh");
        }
    }

    class ProductManager //ProductDal'ı burada kullanmak için
    {
        private IProductDal _productDal; //Ef çalıştıran IProductDal enjekte ettik 
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //Business Code
        public void Save()
        {
            _productDal.Save();
        }
    }

   
}
