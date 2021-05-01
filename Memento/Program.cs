using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda 

            Book book = new Book //bir kitabı yazdık
            {
                Isbn = "12345",
                Title = "Sefiller",
                Author = "Victor Hugo"      
            };
            book.ShowBook();

            //değiştirmek istersek

            CareTaker history = new CareTaker(); 
            history.Memento = book.CreateUndo();

            book.Isbn = "54321";
            book.Title = "VICTOR HUGO";
            
            book.ShowBook();

            book.RestoreFromUndo(history.Memento); //değişikliklerden vazgeçersek           Memento = hafıza
            book.ShowBook();

            Console.ReadLine();
        }
    }

    //yayınevindeki bir kitabın özelliklerinin değişeceğini varsayalım oradan bu deseni yürütelim

    class Book //kitapta olabilecek özellikleri yazıyoruz
    {
        private string _title; //desenimize ait özellikleri get ve set edebileceğimiz bir yapıya çevirdik
        private string _author;
        private string _ısbn;
        private DateTime _lastEdited; //bir nesneye ihtiyacımız olduğundan bunu yazdık
        public string Title
        {
            get { return _title; }
            set 
            {
                _title = value;
                SetLastEdited();

            }
        }
        public string Author
        {
            get { return _author; }
            set 
            { 
                _author = value;
                SetLastEdited();
            }
        }
        public string Isbn
        {
            get { return _ısbn; }
            set 
            { 
                _ısbn = value;
                SetLastEdited();
            }
        }

        private void SetLastEdited() //methodu class'ın içindeki özelliklerde etkinleştireceğimiz için buraya yazdık
        {
            _lastEdited = DateTime.UtcNow;
        }

        public Memento CreateUndo() //geriye almak için bir hafıza oluşturduk
        {
            return new Memento(_ısbn,_title,_author,_lastEdited);
        }

        public void RestoreFromUndo(Memento memento) //değişiklikleri iptal etmek için
        {
            _title = memento.Title;
            _ısbn = memento.Isbn;
            _author = memento.Author;
            _lastEdited = memento.LastEdited;
        }

        public void ShowBook() //kitap bilgileri console kısmında gözükmesi için
        {
            Console.WriteLine("{0},{1},{2} edited : {3}", Isbn,Title,Author,_lastEdited);
        }
    }

    class Memento //yukarıda get-set ettiğimiz özellikleri buraya yazacağız
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public DateTime LastEdited { get; set; }

        public Memento(string isbn, string title, string author, DateTime lastEdited) //yapımızı oluşturduk
        {
            Isbn = isbn;
            Title = title;
            Author = author;
            LastEdited = lastEdited;
        }
    }

    class CareTaker //yeni nesne oluşturulup eski değeri göstermesi adına 
    {
        public Memento Memento { get; set; }
    }
}
