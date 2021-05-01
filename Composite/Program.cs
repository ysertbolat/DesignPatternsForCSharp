using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            //bütün hiyerarşi sistemini yazdıktan sonra buraya geçiyoruz

            Employee yusuf = new Employee { Name = "Yusuf Sertbolat" }; //öncelikle employee'leri çağırıyoruz
            Employee recep = new Employee { Name = "Recep Sertbolat" };

            yusuf.AddSubordinate(recep); // alt çalışan ekleme işlemi

            Employee kadir = new Employee { Name = "Kadir Sertbolat" };
            yusuf.AddSubordinate(kadir);

            Contractor sadık = new Contractor {Name ="Sadık Özkan" }; //kadir alt çalışanına bir alt çalışan eklemek için
            kadir.AddSubordinate(sadık);

            Employee rabia = new Employee { Name = "Rabia Sertbolat" };
            yusuf.AddSubordinate(rabia);

            //bunların hepsi hiyerarşik model içindi şimdi isa alt satırlarda ekrana çizeceğiz
            Console.WriteLine(yusuf.Name); //kendi ismimiz görünmesi için
            foreach (Employee manager in yusuf) //yöneticinin alt çalışanlarının görünmesi için
            {
                Console.WriteLine("  {0}",manager.Name);
                foreach (IPerson employee in manager) //eğer alt çalışana alt çalışan eklemeseydik Employee yazabilirdik ama eklediğimiz için IPerson dememiz daha uygun olur
                {
                    Console.WriteLine("    {0}",employee.Name);
                }
            }

            Console.ReadLine();
        }
    }

    interface IPerson //öncelikle kurum çalışanlarıyla ilgili bir bölüm hazırlıyoruz
    {
        string Name { get; set; }
    }

    class Contractor:IPerson //kurumun tedarikçisini de yazabilirsiniz bunu da alt çalışanlara bağlayabilirsiniz
    {
        public string Name { get; set; }
    }

    class Employee :IPerson, IEnumerable<IPerson> //ayarladığımız çalışan kısmına bir inheritance atadık
    {
        List<IPerson> _subordinates = new List<IPerson>(); //hiyerarşik yapı için bir list kurduk ve subordinate dedik(subordinate = hiyerarşik yapıda alt nesne demek)

        public void AddSubordinate(IPerson person) //hiyerarşiyi uygulamak için bu methodları yazdık
        {
            _subordinates.Add(person); //implemente işlemi
        }

        public void RemoveSubordinate(IPerson person)
        {
            _subordinates.Remove(person); //implemente işlemi
        }

        public IPerson GetSubordinate(int index) //nesneya ulaşabilecek ortamı yazdık
        {
            return _subordinates[index];
        }
        public string Name { get; set; }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var subordinate in _subordinates) //subordinate'i döndürebilsin diye
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



}
