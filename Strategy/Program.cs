using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{

    //bir bankanın farklı müşteri tiplerine göre farklı kredi hesaplama sistemini baz alarak bir proje yapacağız
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.CreditCalculatorBase = new After2010CreditCalculator(); //buraya Before2010CreditCalculator yazsaydık 2010 öncesi heasplama çıkardı
            customerManager.SaveCredit();

            Console.ReadLine();
        }
    }


    abstract class CreditCalculatorBase //base class'ımızı oluşturuyoruz
    {
        public abstract void Calculate();
    }

    class Before2010CreditCalculator : CreditCalculatorBase //2010 öncesi için hesaplama
    {
        public override void Calculate()
        {
            Console.WriteLine("Credit calculated using before2010");
        }
    }

    class After2010CreditCalculator : CreditCalculatorBase //2010 sonrası için hesaplama
    {
        public override void Calculate()
        {
            Console.WriteLine("Credit calculated using after2010");
        }
    }

    class CustomerManager //iş katmanında bunları kullandığımızı varsayalım
    {
        public CreditCalculatorBase CreditCalculatorBase { get; set; } //base üzerinden buraya ne yazarsak o çıkacaktır
        public void SaveCredit()
        {
            Console.WriteLine("Customer manager business");
            CreditCalculatorBase.Calculate();
        }
    }
}
