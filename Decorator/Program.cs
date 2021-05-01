using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var personalCar = new PersonalCar {Make = "BMW", Model = "3,20", HirePrice = 2500 }; //bu normal fiyatlar

            SpecialOffer specialOffer = new SpecialOffer(personalCar);
            specialOffer.DiscountPercentage = 10; //özel teklife özellik yazdığımızda burda set etmemiz gerek

            Console.WriteLine("Concrete : {0}", personalCar.HirePrice);
            Console.WriteLine("Special Offer : {0}", specialOffer.HirePrice);

            Console.ReadLine();
        }
    }

    abstract class CarBase //arabaların özelliklerini belirlemek için abstract class oluşturduk
    {
        public abstract string Make { get; set; } //özellikleri de belirli yerlerde değiştireceğimiz için abstract yaptık
        public abstract string Model { get; set; }
        public abstract decimal HirePrice { get; set; }
    }

    class PersonalCar : CarBase // kişisel araçları carbase ile implemente etmemiz gerek
    {
        public override string Make { get; set; }
        public override string Model { get; set; }
        public override decimal HirePrice { get; set; }
    }

    class CommercialCar : CarBase //kiralık araçları carbase ile implemente etmemiz gerek
    {
        public override string Make { get; set; }
        public override string Model { get; set; }
        public override decimal HirePrice { get; set; }
    }

    //bundan sonra bizim özellikleri değiştirmek adına bir Decorator'e ihtiyacımız var

    abstract class CarDecoratorBase : CarBase
    {
        private CarBase _carBase; //carBase'e bağımlı hale getirmek için

        protected CarDecoratorBase(CarBase carBase)
        {
            _carBase = carBase;
        }
    }

    class SpecialOffer : CarDecoratorBase //özel teklifimizi buraya yazacağız
    {
        public int DiscountPercentage { get; set; } //istersek özel tekliflerde yeni özellik de ekleyebiliriz

        private readonly CarBase _carBase;
        public SpecialOffer(CarBase carBase) : base(carBase) //hangi araç türüyle çalışacaksak özel teklifimize onu göndermiş olduk
        {
            _carBase = carBase;
        }
        public override string Make { get; set; }
        public override string Model { get; set; }
        public override decimal HirePrice 
        { 
            get { return _carBase.HirePrice -_carBase.HirePrice * DiscountPercentage/100; } //bu bizim özel teklifimiz oluyor bunu istediğimiz gibi değiştirebiliriz
            set { } 
        }
    }

    //bu bizim basit bir decorator sistemimiz arzu ederseniz yeni değişiklikler de ekleyebilirsiniz (CarDecoratoBase'e bağlı kalarak)
}
