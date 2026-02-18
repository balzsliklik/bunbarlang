using System;

namespace bunbarlang
{
    internal class Penzfeldobas
    {
        private Random rnd = new Random();

        public void Jatszik(Jatekos jatekos)
        {
            Console.Clear();
            Console.WriteLine("=== PÉNZFELDOBÁS ===");
            Console.WriteLine($"Egyenleged: {jatekos.Penz} Ft");

            Console.Write("Tét: ");
            if (!int.TryParse(Console.ReadLine(), out int tet) || !jatekos.VanElegPenze(tet))
            {
                Console.WriteLine("Érvénytelen tét!");
                Console.ReadKey();
                return;
            }

            Console.Write("Fej vagy Írás? (f/i): ");
            string tipp = Console.ReadLine().ToLower();

            jatekos.Penz -= tet;

            Console.Write("Pörög...");
            System.Threading.Thread.Sleep(1000); 

            bool fej = rnd.Next(0, 2) == 0;
            string eredmeny = fej ? "f" : "i";

            Console.WriteLine($"\nAz eredmény: {(fej ? "Fej" : "Írás")}");

            if (tipp == eredmeny)
            {
                Console.WriteLine($"NYERTÉL! +{tet * 2} Ft");
                jatekos.Penz += tet * 2;
            }
            else
            {
                Console.WriteLine("Vesztettél!");
            }

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz...");
            Console.ReadKey();
        }
    }
}