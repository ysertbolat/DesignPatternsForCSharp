using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new Log4NetAdapter()); //buraya parametre olarak EdLogger yazarsak bizim yazdığımız işlem okunur Log4Net yazarsak adapte ettiğimiz işlem okunur
            productManager.Save();

            Console.ReadLine();
        }
    }
    
    class ProductManager //öncelikle loglama çağırabileceğimiz bir class oluşturuyoruz
    {
        
        private ILogger _logger; //logladığımız kısmı buraya yazıyoruz
        public ProductManager(ILogger logger) //burada dependency injection ile Ilogger'a bağlılığımızı iletmiş olduk
        {
            _logger = logger;
        }
        public void Save()
        {
            _logger.Log("User Data");
            Console.WriteLine("Saved!");
        }
    }

    interface ILogger
    {
        void Log(string message);
    }

    class EdLogger : ILogger //loglama işlemimizi uyguladığımız bir class yazdık
    {
        public void Log(string message)
        {
            Console.WriteLine("Logged, {0}", message);
        }
    }

    //bu yazdığımızı sadece bir işlem için yazdık birçok işlemle yazdığımız zaman okunması zor bir kod olacağından adapter desenini devreye sokacağız
   
        //Nuget'den indirdiğimizi varsaydığımız kod olarak şunu yazdık
    class Log4Net //bunu bir nuget paketi olarak varsaydığımız için buna dokunamıyoruz değiştirme yapamıyoruz eski kodlarımıza da yeni bir şey ekleyince değişim yapamayız çünkü SOLID'e aykırı
    {
        public void LogMessage(string message) //ismi değiştirmemiz gerekiyor karışıklık olmasın diye
        {
            Console.WriteLine("Logged with Log4Net, {0}", message);
        }
    }

    //bundan sonra hiçbir şey yapamıyor gibi gözükebiliriz işte tam da burada Adapter deseni devreye giriyor

    class Log4NetAdapter : ILogger //farklı yerden gelen işlemi kendi projemize adapte etmek için kurduğumuz desen
    {
        public void Log(string message)
        {
            Log4Net log4Net = new Log4Net(); //bunları implemente ettikten sonra artık bizim projemize Log4Net adapte olmuş oldu dolayısıyla artık onu çağırabiliriz
            log4Net.LogMessage(message);
        }
    }
}
