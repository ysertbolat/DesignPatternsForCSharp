using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda ise 

            Manager manager = new Manager(); //bütün ele alacak kişileri çağırdık
            VicePresident vicePresident = new VicePresident();
            President president = new President();

            manager.SetSuccessor(vicePresident); //manager ve vice president'ın üstlerini belirledik
            vicePresident.SetSuccessor(president);

            Expense expense = new Expense { Detail = "Training", Amount = 1005 }; //harcamamızı belirliyoruz
            manager.HandleExpense(expense);

            Console.ReadLine();
        }
    }

    class Expense //öncelikle harcamalarla ilgili bir class oluşturduk (detayı, bütçesi)
    {
        public string Detail { get; set; }
        public decimal Amount { get; set; }

    }

    abstract class ExpenseHandlerBase //daha sonra harcamayı ele alacak kişileri ve onların üstlerine devretme olayını buraya yazacağız
    {
        protected ExpenseHandlerBase Successor;
        public abstract void HandleExpense(Expense expense);

        public void SetSuccessor(ExpenseHandlerBase successor)
        {
            Successor = successor;
        }
    }
    
    //son olarak harcamalarımızı yönetecek kişilerin sınıflarını oluşturup onları base ile implemente edip ele alma şartlarını yazıyoruz

    class Manager : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount <=100) //100 altındaysa
            {
                Console.WriteLine("Manager handled the expense");
            }
            else if(Successor != null) //belli değilse
            {
                Successor.HandleExpense(expense); //successor handle etsin
            }
        }
    }

    class VicePresident : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount >= 100 && expense.Amount <= 1000) //100 ile 1000 arasındaysa
            {
                Console.WriteLine("Vice President handled the expense");
            }
            else if (Successor != null) //belli değilse
            {
                Successor.HandleExpense(expense); //successor handle etsin
            }
        }
    }

    class President : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount >= 1000) //1000'in üzerindeyse
            {
                Console.WriteLine("President handled the expense");
            }
            else if (Successor != null) //belli değilse
            {
                Successor.HandleExpense(expense); //successor handle etsin
            }
        }
    }

}
