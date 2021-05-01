using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new Factory2()); //artık abstract design şeklinde methodumuzu çalıştırabiliriz
            productManager.GetAll();
            Console.ReadLine();
        }
    }

    public abstract class Logging //bir tane abstract class oluşturuyoruz 
    {
        public abstract void Log(string message);
    }

    public class Log4NetLogger : Logging //bunu inheritance edip loglama yapacağımız yöntemi yazıyoruz 
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with Log4net");
        }
    }

    public class NLogger : Logging //loglama yapacağımız başka bir yöntem
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with nLogger");
        }
    }

    public abstract class Caching //farklı bir abstract classımız
    {
        public abstract void Cache(string data);
    }

    public class MemCache : Caching //cachleme yaptığımız bir class (inheritance)
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache");
        }
    }

    public class RedisCache : Caching //cachleme yaptığımız başka bir class (inheritance) sistem hiç bozulmayacak böyle devam edecek istediğiniz kadar yapılabilir
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with RedisCache");
        }
    }

    //abstract classları anladığımıza göre şimdi ise bunlariçin nesne üretecek fabrikalara ihtiyacımız var

    public abstract class CrossCuttingConcernsFactory //yeni fabrikalar üretmek için bunu yazıyoruz daha sonra bunu fabrikalara inheritance ediyoruz
    {
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    public class Factory1 : CrossCuttingConcernsFactory //yeni fabrikamızda üreteceğimiz kodları yazdık
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new Log4NetLogger();
        }
    }

    public class Factory2 : CrossCuttingConcernsFactory //yeni diğer fabrikamızda üreteceğimiz kodları yazdık
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new NLogger();
        }
    }

    //şimdi ise bir clientt'a ihtiyacımız var bunun için de bbir iş katmanı oluşturalım

    public class ProductManager //iş katmanını cachelemek ve loglamak için içine method dışında bir şeyler daha yazdık
    {
        private Logging _logging; //loglama ve cacheleme için bunları da çağırdık
        private Caching _caching;

        private CrossCuttingConcernsFactory _crossCuttingConcernsFactory; //bunu yazdıktan sonra oluşturucuyu oluşturun 

        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory) //method olarak yazmış olduk
        {
            _crossCuttingConcernsFactory = crossCuttingConcernsFactory;
            _logging = _crossCuttingConcernsFactory.CreateLogger(); //bunları da methodllaştırdık
            _caching = _crossCuttingConcernsFactory.CreateCaching();
        }

        public void GetAll() //buraya bütün iş katmanında her şeyi çağırdıktan sonra methodları çalıştırabiliriz
        {
            _logging.Log("Logged!");
            _caching.Cache("Data");
            Console.WriteLine("Products Listed!");
        }
    }

}
