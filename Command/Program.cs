using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        //ctrl + Z komutların geri alınmasını gösterir

        //bu desen için sipariş takip sisteminden örnek oluşturacağız
        static void Main(string[] args)
        {
            //ana kısımda

            StockManager stockManager = new StockManager(); //stok işlemlerini tanımladık
            BuyStock buy = new BuyStock(stockManager); //burada parametre gerekiyor
            SellStock sell = new SellStock(stockManager);

            StockController stockController = new StockController(); //kontrolerı çağırdık
            stockController.TakeOrder(buy); //siparişleri verdik
            stockController.TakeOrder(sell);
            stockController.TakeOrder(buy);

            stockController.PlaceOrders();

            Console.ReadLine();
        }
    }

    class StockManager
    {
        private string _name = "Laptop"; //stok yapılacak ürünümüzü yazıyoruz
        private int _quantity = 10;

        public void Buy() //alım/satım methodlarımızı yazıyoruz
        {
            Console.WriteLine("Stock : {0} {1} bought!", _name,_quantity);
        }

        public void Sell()
        {
            Console.WriteLine("Stock : {0} {1} sold!", _name, _quantity);
        }
    }

    //şimdi komutları gerçekleştirmek için bir class daha yazıyoruz

    interface IOrder //komutların base'ini oluşturmayı unutmayın
    {
        void Execute();
    }

    //komutları nesneleştireceğiz 
    class BuyStock : IOrder
    {
        private StockManager _stockManager; //komutları çalıştırmak için stockmanager'ı enjekte ettik
        public BuyStock(StockManager stockManager)
        {
            _stockManager = stockManager;
        }
        public void Execute()
        {
            _stockManager.Buy();
        }
    }

    class SellStock : IOrder
    {
        private StockManager _stockManager; //komutları çalıştırmak için stockmanager'ı enjekte ettik
        public SellStock(StockManager stockManager)
        {
            _stockManager = stockManager;
        }
        public void Execute()
        {
            _stockManager.Sell();
        }
    }

    //komutları kontrol edecek bir nesneye ihtiyacımız var 

    class StockController
    {
        List<IOrder> _orders = new List<IOrder>();
        public void TakeOrder(IOrder order) //alım satımı buradan kontrol edeceğiz 
        {
            _orders.Add(order);
        }

        public void PlaceOrders() //kullanıcının işi bittiğinde bunu çalıştırması için
        {
            foreach (var order in _orders)
            {
                order.Execute();
            }

            _orders.Clear();
        }
    }
}
