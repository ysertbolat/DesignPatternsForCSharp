using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    class Program
    {
        //bir şirketin maaş artışı ve ödeme işlemlerini visitor deseni ile baz alan sistem kurmak için şunları yapacağız
        static void Main(string[] args)
        {
            //ana kısımda 

            Manager yusuf = new Manager { Name = "Yusuf", Salary = 1000 }; //yöneticileri tanımladık
            Manager kadir = new Manager { Name = "Kadir", Salary = 950 };

            Worker rabia = new Worker { Name = "Rabia", Salary = 500 }; //işçileri tanımladık
            Worker recep = new Worker { Name = "Recep", Salary = 650 };

            yusuf.Subordinates.Add(kadir); //alt çalışanları tanımladık
            kadir.Subordinates.Add(rabia);
            kadir.Subordinates.Add(recep);

            OrganisationalStructure organisationalStructure = new OrganisationalStructure(yusuf); //ödeme ve maaş sistemini tanımladık
            PayrollVisitor payrollVisitor = new PayrollVisitor();
            PayriseVisitor payriseVisitor = new PayriseVisitor();

            organisationalStructure.Accept(payrollVisitor); //maaş ve ödemeleri arttırmayı onayladık
            organisationalStructure.Accept(payriseVisitor);

            Console.ReadLine();
        }
    }

    class OrganisationalStructure //bir kurum yapısı oluşturuyoruz
    {
        public EmployeeBase Employee; //çalışanları enjekte ettik
        public OrganisationalStructure(EmployeeBase firstEmployee)
        {
            Employee = firstEmployee;
        }

        public void Accept(VisitorBase visitor)
        {
            Employee.Accept(visitor);
        }

    }

    abstract class EmployeeBase //çalışanların base'i
    {
        public abstract void Accept(VisitorBase visitor);
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    class Manager : EmployeeBase //çalışanları oluşturup base ile implemente ettik
    {
        public Manager()
        {
            Subordinates = new List<EmployeeBase>();
        }
        public List<EmployeeBase> Subordinates { get; set; }
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
            foreach (var employee in Subordinates) //bütün alt çalışanları gezmesi için
            {
                employee.Accept(visitor);
            }
        }
    }

    class Worker : EmployeeBase
    {
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
    abstract class VisitorBase //ziyaretçi eklememiz gerek
    {
        public abstract void Visit(Worker worker);
        public abstract void Visit(Manager manager);
    }

    class PayrollVisitor : VisitorBase //ödeme kısmı için
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} paid {1}", worker.Name, worker.Salary);
        }

        public override void Visit(Manager manager)
        {
            {
                Console.WriteLine("{0} paid {1}", manager.Name, manager.Salary);
            }
        }
    }
    class PayriseVisitor : VisitorBase //maaş artışı için
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased to {1}", worker.Name, worker.Salary * (decimal)1.1);
        }

        public override void Visit(Manager manager)
        {
            {
                Console.WriteLine("{0} salary increased to {1}", manager.Name, manager.Salary * (decimal)1.2);
            }
        }
    }
}
    

