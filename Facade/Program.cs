using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.Save();

            Console.ReadLine();
        }
    }

    class Logging: ILogging //logging için bir class ve interface oluşturuyoruz
    {
        public void Log()
        {
            Console.WriteLine("Logged!");
        }
    }

    interface ILogging
    {
        void Log();
    }

    class Caching : ICaching //caching için bir class ve interface oluşturuyoruz
    {
        public void Cache()
        {
            Console.WriteLine("Cached!");
        }
    }

    interface ICaching
    {
        void Cache();
    }

    class Authorize : IAuthorize //authorizing için bir class ve interface oluşturuyoruz
    {
        public void CheckUser()
        {
            Console.WriteLine("User checked!");
        }
    }

    interface IAuthorize
    {
        void CheckUser();
    }

    class Validation : IValidate //validation için bir class ve interface oluşturuyoruz (yeni bir işlem eklemek istersek diye CrossCuttingConcernFacade'e ekliyoruz)
    {
        public void Validate()
        {
            Console.WriteLine("Validated!");
        }
    }

    interface IValidate
    {
        void Validate();
    }

    class CustomerManager //bunları uygulamak için çağırmamız gereken bir class yaratıp şunları yazıyoruz
    {
        private CrossCuttingConcernFacade _concerns;
        public CustomerManager() //burada enjekte ediyoruz
        {
            _concerns = new CrossCuttingConcernFacade();
        }

        public void Save() //daha sonra bir method üretiyoruz ve içine yazdığımız işlemleri de yazabiliyoruz
        {
            _concerns.Logging.Log();
            _concerns.Caching.Cache();
            _concerns.Authorize.CheckUser();
            _concerns.Validation.Validate(); //yeni bir işlem ekleyince buraya da yazıyoruz

            Console.WriteLine("Saved!");
        }
    }

    class CrossCuttingConcernFacade //bunları böyle tek tek kullanmaktansa bir sınıf içersinde kullanabiliriz buna facade deseni deriz
    {
        public ILogging Logging;
        public ICaching Caching;
        public IAuthorize Authorize;
        public IValidate Validation; //yeni bir işlem ekleyince buraya da yazıyoruz

        public CrossCuttingConcernFacade()
        {
            Logging = new Logging();
            Caching = new Caching();
            Authorize = new Authorize();
            Validation = new Validation(); //yeni bir işlem ekleyince buraya da yazıyoruz
        }
    }
}
