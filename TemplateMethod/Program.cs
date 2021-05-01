using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            //ana kısımda

            ScoringAlgorithm algorithm;
            Console.WriteLine("Mans"); 
            algorithm = new MensSccoringAlgorithm();
            Console.WriteLine(algorithm.GenerateScore(8,new TimeSpan(0,2,34))); //erkekler

            Console.WriteLine("Women");
            algorithm = new WomenSccoringAlgorithm();
            Console.WriteLine(algorithm.GenerateScore(8, new TimeSpan(0, 2, 34))); //kadınlar 

            Console.WriteLine("Childrens");
            algorithm = new ChildrensSccoringAlgorithm();
            Console.WriteLine(algorithm.GenerateScore(8, new TimeSpan(0, 2, 34))); //çocuklar

            Console.ReadLine();
        }
    }

    //bir oyun varsayalım herkese hitap eden ama erkek kadın çocuk hesaplarının farklılık gösterdiği

    abstract  class ScoringAlgorithm //template method'u içerisinde barındıracak bir hesaplama algoritması oluşturuyoruz 
    {
        public int GenerateScore(int hits, TimeSpan time) //puan yönetimini yazıyoruz
        {
            int score = CalculateBaseScore(hits);
            int reduction = CalculateReduction(time);
            return CalculateOverallScore(score, reduction);
        }

        public abstract int CalculateOverallScore(int score, int reduction);

        public abstract int CalculateReduction(TimeSpan time);


        public abstract int CalculateBaseScore(int hits);
    }

    //şimdi algoritmamızı erkek, kadın ve çocuklar için ayrı ayrı implemente edip alacakları puan sisteminde farklılık gösteriyoruz

    class MensSccoringAlgorithm : ScoringAlgorithm
    {
        public override int CalculateBaseScore(int hits)
        {
            return hits * 100;
        }

        public override int CalculateOverallScore(int score, int reduction)
        {
            return score - reduction;
        }

        public override int CalculateReduction(TimeSpan time)
        {
            return (int)time.TotalSeconds / 5;
        }
    }

    class WomenSccoringAlgorithm : ScoringAlgorithm
    {
        public override int CalculateBaseScore(int hits)
        {
            return hits * 100;
        }

        public override int CalculateOverallScore(int score, int reduction)
        {
            return score - reduction;
        }

        public override int CalculateReduction(TimeSpan time)
        {
            return (int)time.TotalSeconds / 3;
        }
    }
    class ChildrensSccoringAlgorithm : ScoringAlgorithm
    {
        public override int CalculateBaseScore(int hits)
        {
            return hits * 80;
        }

        public override int CalculateOverallScore(int score, int reduction)
        {
            return score - reduction;
        }

        public override int CalculateReduction(TimeSpan time)
        {
            return (int)time.TotalSeconds / 2;
        }
    }
}
