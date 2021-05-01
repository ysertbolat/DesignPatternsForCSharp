using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreditManager manager = new CreditManager(); //yazdığımız kredi yönetimini buraya yazıp uyguluyoruz //bu eski yöntem
            CreditBase manager = new CreditManagerProxy(); //bu ise proxy için uygulayacağımız yöntem 
            
            Console.WriteLine(manager.Calculate()); //bunlar iki tarafda da aynı
            Console.WriteLine(manager.Calculate());

            Console.ReadLine();

        }
    }

    abstract class CreditBase //krediyle ilgili temel sınıfımızı oluşturuyoruz
    {
        public abstract int Calculate();
    }

    class CreditManager : CreditBase //kredi hesaplamalarla ilgili bir sınıf oluşturduk
    {
        public override int Calculate() //basit bir hesaplama kodu yazıyoruz 
        {
            int result = 1;
            for (int i = 1; i < 5; i++)
            {
                result *= i;
                Thread.Sleep(1000);
            }
            return result;
        }
    }

    //bu temel bir cachelemedir bunları kısaltmak için bir Proxy deseni yazabiliriz

    class CreditManagerProxy : CreditBase //çalışacağımız methodaları çağırıp kodlamamızı yaptık şimdi ana kısma geçebiliriz
    {
        private CreditManager _creditManager;
        private int _cachedValue;
        public override int Calculate()
        {
            if (_creditManager == null)
            {
                _creditManager = new CreditManager();
                _cachedValue = _creditManager.Calculate();
            }

            return _cachedValue;
        }
    }
    //bundan sonra 2. defa çağrılan method anında gelmiş olacak uygulama gereksiz hesaplamayı yapmamış olacak
}
