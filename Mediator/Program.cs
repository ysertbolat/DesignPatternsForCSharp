using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    //canlı online eğitim verdiğimizi düşünelim öğretmen-öğrenci arasındaki iletişimi sağlayacak bir desen yapacağız
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda 

            Mediator mediator = new Mediator(); //iletişimi tanımladık
            Teacher engin = new Teacher(mediator); //öğretmeni tanımladık
            engin.Name = "Engin";
            
            mediator.Teacher = engin;
            
            Student yusuf = new Student(mediator); //1. öğrenciyi tanımladık
            yusuf.Name = "Yusuf";
           

            Student hatice = new Student(mediator); //2. öğrenciyi tanımladık
            hatice.Name = "Hatice";

            mediator.Students = new List<Student> { yusuf, hatice }; //liste şeklinde istediğimiz için öğrencileri liste şeklinde tanımladık

            engin.SendNewImageUrl("slide1.jpg");
            engin.ReceiveQuestion("is it true?",hatice);

            Console.ReadLine();
        }
    }

     abstract class CourseMember //kursumuzun ortak özelliklerini barındıran yer
    {
        protected Mediator Mediator;

        public CourseMember(Mediator mediator) //öğretmenin ve öğrencinin  bir mediatora ihtiyacı olduğu için constructor vasıtasıyla mediator oluşturduk ve her ikisine de implemente ettik
        {
            Mediator = mediator;
        }
    }

    class Teacher : CourseMember //öğretmen için
    {
        public string Name { get; internal set; }

        public Teacher(Mediator mediator) : base(mediator)
        {
        }

        internal void ReceiveQuestion(string question, Student student)
        {
            Console.WriteLine("Teacher received a question from {0}, {1}", student.Name, question); //öğrenci sorusunu sorduğu için
        }

        public void SendNewImageUrl(string url) //öğretmenin slayt göndermesi için
        {
            Console.WriteLine("Teacher changed slide : {0}", url);
            Mediator.UpdateImage(url);
        }

        public void AnswerQuestion(string answer, Student student) //sorunun cevabının iletilmesi için
        {
            Console.WriteLine("Teacher answered question {0}, {1}", student.Name, answer);
        }
    }

    class Student : CourseMember //öğrenci için
    {
        public Student(Mediator mediator) : base(mediator)
        {
        }

        internal void ReceiveImage(string url) //slaytı almak için
        {
            Console.WriteLine("{1} received image : {0}", url,Name);
        }

        internal void ReceiveAnswer(string answer) //sorunun cevabını almak için
        {
            Console.WriteLine("Student received answer {0}", answer);
        }

        public string Name { get; set; }
    }

    class Mediator //bilgiyi alacak kısım ve tüm nesnelere verecek kısım
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public void UpdateImage(string url) //öğretmenin paylaşacağı bir resmi öğrencilerin almaları için
        {
            foreach (var student in Students)
            {
                student.ReceiveImage(url);
            }

        }

        public void SendQuestion(string question , Student student) //öğretmenin öğrencilerden soru alması için
        {
            Teacher.ReceiveQuestion(question,student);
        }

        public void SendAnswer(string answer, Student student) //öğrencilerin cevabı alması için
        {
            student.ReceiveAnswer(answer);
        }
    }
}
