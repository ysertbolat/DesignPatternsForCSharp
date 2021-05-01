using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullObject
{
    class Program
    {
        //ana kısımda

        //sahte bir loglama oluşturup onu kullanacağız
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(StubLogger.GetLogger()); //artık loglama çalışacaktır ama sahte loglamamız devreye girecektir
            customerManager.Save();
            //buraya kadarki kısım DI ile yapılan kısım

            Console.ReadLine();
        }
    }

    class CustomerManager //müşteri sınıfımızı oluşturuyoruz
    {
        ILogger _logger; //CustomerManager'ı newlediğimizde logger vermesi için logger'ı enjekte ettik
        public CustomerManager(ILogger logger)
        {
            _logger = logger;
        }
        public void Save() //methodumuzu yazdık 
        {
            Console.WriteLine("Saved");
            _logger.Log();
        }
    }

    interface ILogger //Loglama için bir interface yazıyoruz ve bunu iki loglama tipine implemente ediyoruz
    {
        void Log();
    }

    class Log4NetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with Log4Net");
        }
    }

    class NLogLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with NLogLogger");
        }
    }

    class StubLogger : ILogger //sahte loglama sınıfımızı oluşturuyoruz
    {
        private static StubLogger _stubLogger; //singleton oluşturuyoruz
        private static object _lock = new object();

        private StubLogger() { } //singleton için private bir stublogger oluşturduk

        public static StubLogger GetLogger()
        {
            lock (_lock) //burada varsa olanı yoksa yeni bir instance üretip onu döndürecek bir sistem yazdık (singleton ile sahte loglama için )
            {
                if(_stubLogger == null)
                {
                    _stubLogger = new StubLogger();
                }
            }
            return _stubLogger;
        }
        public void Log()
        {

        }
    }

    class CustomerManagerTests //uydurma bir test oluşturduk
    {
        public void SaveTest()
        {

            //burada bizden loglamak için bir tür istiyor ama biz sadece kaydedip kaydetmeyeceğimizi görmek istiyorduk işte buraya sahte bir log göndereceğiz
            CustomerManager customerManager = new CustomerManager(StubLogger.GetLogger()); //artık sahte loglamamız aktif halde
            customerManager.Save();
        }
    }
}
