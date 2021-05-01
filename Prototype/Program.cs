using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer {FirstName = "Yusuf", LastName = "Sertbolat", City = "Bursa", Id = 1}; //ilk müşterimizi oluşturup yazdık

            Customer customer2 = (Customer)customer1.Clone(); //elimizdeki nesneyi kullanarak bir klon oluşturduk ve onun da istediğimiz bilgilerini yazdık
            customer2.FirstName = "Recep";

            Console.WriteLine(customer1.FirstName); //artık iki nesneyi de kullanabiliriz ve klonlanmış oolan nesne ile klonladığımız nesne artık birbirinden farklı nesneler
            Console.WriteLine(customer2.FirstName);

            Console.ReadLine();
        }
    }
    //burada bir prototype nesnesi üzerinden ilerliyor olmamız gerekiyor (müşteri takip sistemindeki müşteri çalışan kişi nesnesi üzerinden ilerleyeceğiz) 

    public abstract class Person //bir prototip nesne oluşturduk ve içine bir kişiye ait olabilecek özelliklerin methodlarını yazdık
    {
        public abstract Person Clone();

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Customer : Person //şimdi person ile inheritance edebileceğimiz bir kişi ekledik ve özelliklerini yazdık
    {
        public string City { get; set; }

        public override Person Clone() //implement ettik
        {
            return (Person)MemberwiseClone(); //klonlama işlemi için bu kodu yazdık
        }
    }

    public class Employee : Person //başka bir kişi grubu
    {
        public decimal Salary { get; set; }

        public override Person Clone() //implement ettik
        {
            return (Person)MemberwiseClone(); //klonlama işlemi için bu kodu yazdık
        }
    }
    

}
