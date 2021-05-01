using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiton
{
    //amacımız farklı markalarla çalışılırken belirtilen camera markaları için aynı instance'ı vermek
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda 

            Camera camera1 = Camera.GetCamera("NIKON"); //kammera markalarını oluşturduk
            Camera camera2 = Camera.GetCamera("NIKON");
            Camera camera3 = Camera.GetCamera("CANON");
            Camera camera4 = Camera.GetCamera("CANON");

            Console.WriteLine(camera1.Id); //onları yazdık
            Console.WriteLine(camera2.Id);
            Console.WriteLine(camera3.Id);
            Console.WriteLine(camera4.Id);

            Console.ReadLine();

        }
    }

    class Camera
    {
        static Dictionary<string, Camera> _cameras = new Dictionary<string, Camera>(); //camera instance'ı listesi oluşturduk
        static object _lock = new object(); //singletondaki gibi obje oluşturduk (markaları if ile oluşturmak için)
        private string brand; //markası için
        public Guid Id { get; set; }
        private Camera() //singletondaki gibi constructor oluşturduk her bir instance için bir guid verecek aynı markalar için aynı guidi oluşturcak
        {
            Id = Guid.NewGuid(); //her instance için guid oluşturduk
        }

        public static Camera GetCamera(string brand) 
        {
            lock (_lock)
            {
                if (!_cameras.ContainsKey(brand)) //eğer kameraların markası yoksa kameralara marka ekleyin demek (! = yoksa anlamı taşır)
                {
                    _cameras.Add(brand, new Camera());
                }
            }
            return _cameras[brand]; //markaya göre bir ID döndürme işlemi
        }
    }
}
