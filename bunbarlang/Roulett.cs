using System;

namespace bunbarlang
{
    internal class Roulette
    {
        private Random rnd = new Random();

        public void Jatszik(Jatekos jatekos)
        {
            Console.Clear();
            Console.WriteLine("=== RULETT ===");
            Console.WriteLine($"Egyenleged: {jatekos.Penz} Ft");

            Console.Write("Tét: ");
            if (!int.TryParse(Console.ReadLine(), out int tet) || !jatekos.VanElegPenze(tet))
            {
                Console.WriteLine("Érvénytelen tét vagy nincs elég pénzed!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nMire fogadsz?");
            Console.WriteLine("p - Piros");
            Console.WriteLine("f - Fekete");
            Console.WriteLine("ps - Páros");
            Console.WriteLine("pt - Páratlan");
            Console.WriteLine("0 - Nulla (35-szörös nyeremény!)");
            Console.Write("Választásod: ");
            string tipp = Console.ReadLine().ToLower();

            jatekos.Penz -= tet;

            Console.WriteLine("\n A golyó pörög...");
            System.Threading.Thread.Sleep(1500); 

            int szam = rnd.Next(0, 37);

            
            bool piros = false;
            int[] pirosSzamok = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            foreach (int p in pirosSzamok)
            {
                if (szam == p) piros = true;
            }

            string szin;

            if (szam == 0)
            {
                szin = "Zöld";
            }
            else
            {
                if (piros == true)
                {
                    szin = "Piros";
                }
                else
                {
                    szin = "Fekete";
                }
            }

            bool nyert = false;
            int szorzo = 2; 

            if (tipp == "p" && szin == "Piros") nyert = true;
            else if (tipp == "f" && szin == "Fekete") nyert = true;
            else if (tipp == "ps" && szam != 0 && szam % 2 == 0) nyert = true;
            else if (tipp == "pt" && szam % 2 != 0) nyert = true;
            else if (tipp == "0" && szam == 0) { nyert = true; szorzo = 36; }

            if (nyert)
            {
                int nyeremeny = tet * szorzo;
                Console.WriteLine($"GRATULÁLOK! Nyertél: {nyeremeny} Ft");
                jatekos.Penz += nyeremeny;
            }
            else
            {
                Console.WriteLine("Sajnos vesztettél.");
            }


            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Aktuális egyenleged: {jatekos.Penz} Ft");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("\nNyomj meg egy gombot a visszatéréshez...");
            Console.ReadKey();
        }
    }
}