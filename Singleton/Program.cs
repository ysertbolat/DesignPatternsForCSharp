using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Program
    {
        //biz singleton patternini direkt yazmayız factory design patterni ile birlikte kullanırız ayrıca artık bunu IOS'ler (ninject gibi) ele alabiliyor
        static void Main(string[] args)
        {
            var customerManager = CustomerManager.CreateAsSingleton(); //artık newlemeden bu şekilde methodumuzu çağırabilir ve işlemleri uygulayabiliriz
            customerManager.Save();
        }
    }

    class CustomerManager //bir class kuruyoruz
    {
        private static CustomerManager _customerManager; //yöneteciğimiz bir nesne yazıyoruz 
        static object _lockObject = new object();
        private CustomerManager() //private bir constructor yazıyoruz
        {

        }

        public static CustomerManager CreateAsSingleton() //singleton örneğini oluşturacak method yazıyoruz 
        {
            lock (_lockObject)
            {
                if (_customerManager == null) //eğer customermanager oluşturulmamışsa oluştur demek
                {
                    _customerManager = new CustomerManager();
                }

                return _customerManager; //oluşturulmuşsa buradan devam et demek
            }
        }

        public void Save() //bu operasyona artık customermanager'da erişebiliriz
        {
            Console.WriteLine("Saved!!");
        }
    }
}
