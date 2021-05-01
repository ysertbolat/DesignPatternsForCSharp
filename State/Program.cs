using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda
            Context context = new Context();
            ModifiedState modified = new ModifiedState();
            modified.DoAction(context);
            DeletedState deleted = new DeletedState();
            deleted.DoAction(context);

            Console.WriteLine(context.GetState().ToString()); //context'in tüm durumlardaki durumunu öğrenmek için onu tüm durumlarda setstate etmemiz gerek
            Console.ReadLine();
        }
    }

    //bir müzik çaların durduğu devam ettiği durumları varsayıp desenimizi ona göre oluşturacağız 

    interface IState //durumu yönetmek için interfface oluşturuyoruz 
    {
        void DoAction(Context context); //context adında parametre atıyoruz 
    }

    class ModifiedState : IState //durumu düzeltmek için
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State : Modified!");
            context.SetState(this); //modified'ı setstate'e götürmek için
        }

        public override string ToString()
        {
            return "Modified";
        }
    }

    class DeletedState : IState //durumu silmek için
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State : Deleted!");
            context.SetState(this); //deleted'ı setstate'e götürmek için
        }

        public override string ToString()
        {
            return "Deleted";
        }
    }

    class AddedState : IState //duruma eklemek için
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State : Added!");
            context.SetState(this); //added'ı setstate'e götürmek için
        }

        public override string ToString()
        {
            return "Added";
        }
    }

    class Context
    {
        private IState _state;
        public void SetState(IState state) //contextin durumunu oluşturduk
        {
            _state = state;
        }

        public IState GetState() //contextin durumunu döndürdük
        {
            return _state;
        }
    }
}
