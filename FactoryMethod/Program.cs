using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(new LoggerFactory2()); //burada artık save işlemimiz çalışacaktır
            customerManager.Save();

            Console.ReadLine();
        }
    }

    public class LoggerFactory : ILoggerFactory //bir fabrika sınıfı üretiyoruz ve onu bir interface ile bağdaştırıyoruz çünkü başka factory ile çalışmak için de çalışmak için
    {
        public ILogger CreateLogger() //factory methodumuz budur bunun için bir interface'e ihtiyaç duyarız
        {
            //Business to decide Factory (iş geliştirip o fabrikanın nasıl bir logger üreteceğine karar vermek demektir)
            return new EdLogger(); //EdLogger'ı kullanmak istediğimiz senaryo için newledik 
        }
    }

    public class LoggerFactory2 : ILoggerFactory //Başka bir fabrika ile çalışmak için 
    {
        public ILogger CreateLogger() 
        {
            //Business to decide Factory
            return new Log4NetLogger(); //ikinci fabrikamız ikinci kurduğumuz işi üretsin 
        }
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger(); //her yeri bununla implemente etmemiz gerekiyor
    }

    public interface ILogger
    {
        void Log(); //basit bir log yazıyoruz 
    }

    public class EdLogger : ILogger //kendi loglama işlemlerimizi yaptığımız bir logger kuruyoruz
    {
        public void Log()
        {
            Console.WriteLine("Logged with EdLogger");
        }
    }

    public class Log4NetLogger : ILogger //ikinci loglama işlemlerini yaptığımız bir iş kurduk bunu ikinci fabrikada çalıştıracağız
    {
        public void Log()
        {
            Console.WriteLine("Logged with Log4NetLogger");
        }
    }

    public class CustomerManager //ana iş yaptığımız yer burası 
    {
        private ILoggerFactory _loggerFactory; //istediğimiz loglamayı çağırıp seçebilmek için
        public CustomerManager(ILoggerFactory loggerFactory) //injection yaptık
        {
            _loggerFactory = loggerFactory;
        }
        public void Save() //kullanmak istediğimiz senaryo için basit bir method yazdık (amacımız ILogger'ı burda kullanmak)
        {
            Console.WriteLine("Saved");
            ILogger logger = _loggerFactory.CreateLogger();  //bunu EdLogger ile newleseydik ona bağımlı olacaktık ama burada biz ILogger'ı kullanmak için ve save methodunu kullanabilmek için bunu yaptık
            logger.Log(); //loglamayı çağırmak için
            //biz burada loggerFactory'e çok bağımlıyız çünkü yeri gelecek biz başka fabrikalarla da çalışmak isteyeceğiz
        }
    }
}
