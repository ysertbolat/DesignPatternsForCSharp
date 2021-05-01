using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(); //bridge desenini uygulamak için
            customerManager.MessageSenderBase = new EmailSender(); //istersek smssender ile de yapabiliriz
            customerManager.UpdateCustomer();

            Console.ReadLine();
        }
    }

    abstract class MessageSenderBase //mesaj göndermeyle ilgili bir base sınıf oluşturduk
    {
        public void Save()
        {
            Console.WriteLine("Message Saved!");
        }
        public abstract void Send(Body body); //bunu farklı şekillerde mesaj gönderme oluşturmak için kullanacağız yani her biri için bir method oluşturmayacağız tek methodla yapacağız
    }

    class Body //mesaj özellikleri için
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class SmsSender : MessageSenderBase //sms ile yollamak için
    {
        public override void Send(Body body)
        {
            Console.WriteLine("{0} was sent via SmsSender",body.Title);
        }
    }
    class EmailSender : MessageSenderBase //email ile yollamak için
    {
        public override void Send(Body body)
        {
            Console.WriteLine("{0} was sent via EmailSender",body.Title);
        }
    }

    //bütün mesaj gönderme işlemlerimizi vs. oluşturduğumuza göre Bridge desenini yapım aşamasına geçebiliriz

    class CustomerManager  //oluşturduğumuz base sınıfı kullanacağımız yer
    {
        public MessageSenderBase MessageSenderBase { get; set; } //bridge deseni
        public void UpdateCustomer()
        {
            MessageSenderBase.Send(new Body {Title = "About the course!" }); //deseni buraya uyguluyoruz
            Console.WriteLine("Customer Updated!"); 
        }
    }
}

