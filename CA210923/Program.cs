using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA210923
{
    enum Species
    {
        Carp,
        Catfish,
        Pike,
        Bass,
        Bream,
        Grasscarp,
        Clownfish,
        Eel,
    }

    class Fish
    {
        private float _weight;
        private bool _weightIsSet = false;
        private bool _predator;
        private bool _predatorIsSet = false;
        private int _top;
        private int _depth;

        public float Weight
        {
            get => _weight;
            set
            {
                if (value < .5F)
                    throw new Exception("hiba: a hal súlya túl alacsony");
                if (value > 40F)
                    throw new Exception("hiba: a hal súlya túl magas");
                if (_weightIsSet && value > _weight * 1.1F)
                    throw new Exception("hiba nem nőhet ennyivel a hal súlya");
                if (_weightIsSet && value < _weight * .9F)
                    throw new Exception("hiba: nem csökkenhet ennyivel a hal súlya");

                _weight = value;
                _weightIsSet = true;
            }
        }
        public bool Predator
        {
            get => _predator;
            set
            {
                if (_predatorIsSet)
                    throw new Exception("hiba: a hal étkezési szokásai nem változnak");
                _predator = value;
                _predatorIsSet = true;
            }
        }
        public int Top
        {
            get => _top;
            set
            {
                if (value < 0)
                    throw new Exception("hiba: hal nem tud fíz felett lebegni :(");
                if (value > 400)
                    throw new Exception("hiba: a hal merülési mélységének felső határa túl magas");
                _top = value;
            }
        }
        public int Depth
        {
            get => _depth;
            set
            {
                if (value < 10)
                    throw new Exception("hiba: mozgási sáv túl keskeny");
                if (value > 400)
                    throw new Exception("hiba: mozgási sáv túl széles");
                _depth = value;
            }
        }
        public Species Species { get; set; }
    }


    class Program
    {
        static Random rnd = new Random();
        static List<Fish> halak = new List<Fish>();

        static void Main()
        {
            InitHalak();
            GetHalakInfo();
            DbRagadozo();
            LegnagyobbHal();
            Db1dot1mMely();
            Console.ReadKey();
        }

        private static void Db1dot1mMely()
        {
            int dbMely = 0;

            foreach (var h in halak)
                if (h.Top <= 110 && 110 <= (h.Top + h.Depth)) dbMely++;

            Console.WriteLine("----------------");
            Console.WriteLine($"Összesen {dbMely} hal képes 1.1m mélységben úszni");
        }

        private static void LegnagyobbHal()
        {
            int legnagyobbHalIndex = 0;

            for (int i = 1; i < halak.Count; i++)
            {
                if (halak[i].Weight > halak[legnagyobbHalIndex].Weight)
                    legnagyobbHalIndex = i;
            }

            Console.WriteLine("----------------");
            Console.WriteLine($"Legnagyobb hal: ");
            GetHalInfo(halak[legnagyobbHalIndex]);

        }

        private static void DbRagadozo()
        {
            int dbRagadozo = 0;
            foreach (var h in halak)
                if (h.Predator) dbRagadozo++;

            Console.WriteLine("----------------");
            Console.WriteLine($"Összesen {dbRagadozo} hal ragadozó");
        }

        private static void GetHalakInfo()
        {
            foreach (var h in halak)
            {
                if (h.Predator) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Green;
                GetHalInfo(h);
            }
            Console.ResetColor();
        }

        static void GetHalInfo(Fish h)
        {
            Console.WriteLine("{0, -9} {1,4} Kg sáv:[{2,3}-{3,3}] cm",
                    h.Species, h.Weight, h.Top, h.Top + h.Depth);
        }

        static void InitHalak()
        {
            for (int i = 0; i < 100; i++)
            {
                halak.Add(new Fish()
                {
                    Species = (Species)rnd.Next(Enum.GetNames(typeof(Species)).Length),
                    Predator = !(rnd.Next(100) < 90),
                    Weight = rnd.Next(1, 81) / 2F,
                    Top = rnd.Next(401),
                    Depth = rnd.Next(10, 401),
                });
            }
        }
    }
}
