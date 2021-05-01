using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda
            
            var customerObserver = new CustomerObserver(); 
            ProductManager productManager = new ProductManager();
            productManager.Attach(customerObserver);
            productManager.Attach(new EmployeeObserver()); //eklemek için
            productManager.Detach(customerObserver);
            productManager.UpdatePrice();

            Console.ReadLine();
        }
    }

    class ProductManager //bir iş sınıfı oluşturuyoruz
    {
        List<Observer> _observers = new List<Observer>(); //gözlemcilerimizi yönetmek için onları listeliyoruz
        public void UpdatePrice()
        {
            Console.WriteLine("Product price changed!");
            Notify();
        }

        public void Attach(Observer observer) //listeye ekleyeceğimiz gözlemciler için
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer) //listeden çıkaracağımız gözlemciler için
        {
            _observers.Remove(observer);
        }

        private void Notify() //aboneleri bilgilendirmek için sadece bu class'ın kullanacağı bir bilgilendirme methodu
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }

    //öncelikle ürün güncellendiğinde bizim bir belli bir observer nesnelerimiz olacak ve bunlar update'e abone olurlarsa çalışacak şeklinde bir sistem kuracağız

    abstract class Observer //observer'i kurduk
    {
        public abstract void Update();
    }

    class CustomerObserver : Observer //müşteri observerı'nı ana observera bağladık (update'e abone yaptık)
    {
        public override void Update()
        {
            Console.WriteLine("Message to Customer : Product price changed!");
        }
    }

    class EmployeeObserver : Observer //başka bir observerı'nı ana observera bağladık (update'e abone yaptık)
    {
        public override void Update()
        {
            Console.WriteLine("Message to Employee : Product price changed!");
        }
    }
}
