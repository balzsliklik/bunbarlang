using System;
using System.Collections.Generic;

namespace bunbarlang
{
    internal class Kartya21
    {
        private Random rnd = new Random();

        private Kartya KartyaHuzas()
        {
            string[] szinek = { "Kör", "Káró", "Treff", "Pikk" };
            string[] nevek = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Bubi", "Dáma", "Király", "Ász" };
            int[] ertekek = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

            int v = rnd.Next(0, nevek.Length);
            return new Kartya(szinek[rnd.Next(0, 4)], nevek[v], ertekek[v]);
        }

        public void Jatszik(Jatekos jatekos)
        {
            Console.Clear();
            Console.WriteLine("=== BLACKJACK ===");
            Console.WriteLine($"Egyenleged: {jatekos.Penz} Ft");
            Console.Write("Kezdő tét: ");

            if (!int.TryParse(Console.ReadLine(), out int alaptet) || !jatekos.VanElegPenze(alaptet))
            {
                Console.WriteLine("Érvénytelen tét vagy nincs elég pénzed!");
                Console.ReadLine();
                return;
            }

            jatekos.Penz -= alaptet;
            List<Kartya> sajatLapok = new List<Kartya> { KartyaHuzas(), KartyaHuzas() };
            List<Kartya> osztoLapok = new List<Kartya> { KartyaHuzas(), KartyaHuzas() };

            bool jatekban = true;
            int aktualisTet = alaptet;

            while (jatekban)
            {
                int sajatPont = PontSzamilas(sajatLapok);

                
                Console.WriteLine("\n-----------------------------------------");
                Console.WriteLine($"Osztó látható lapja: {osztoLapok[0]} (Értéke: {osztoLapok[0].Ertek} pont) + [?]");
                Console.WriteLine($"Saját kezed: {string.Join(", ", sajatLapok)} (Összeg: {sajatPont})");
                Console.WriteLine($"Aktuális tét: {aktualisTet} Ft | Maradék pénzed: {jatekos.Penz} Ft");
                Console.WriteLine("-----------------------------------------");

                if (sajatPont >= 21) break;


                string opciok = "(H) Hit, (S) Stand";
                if (jatekos.VanElegPenze(aktualisTet)) opciok += ", (D) Double";
                if (sajatLapok.Count == 2 && sajatLapok[0].Ertek == sajatLapok[1].Ertek && jatekos.VanElegPenze(aktualisTet)) opciok += ", (X) Split";

                Console.Write($"\nVálasztásod {opciok}: ");
                string valasz = Console.ReadLine().ToLower();

                if (valasz == "h")
                {
                    sajatLapok.Add(KartyaHuzas());
                }
                else if (valasz == "s")
                {
                    jatekban = false;
                }
                else if (valasz == "d" && jatekos.VanElegPenze(aktualisTet))
                {
                    jatekos.Penz -= aktualisTet;
                    aktualisTet *= 2;
                    sajatLapok.Add(KartyaHuzas());
                    jatekban = false;
                }
                else if (valasz == "x" && sajatLapok.Count == 2 && sajatLapok[0].Ertek == sajatLapok[1].Ertek && jatekos.VanElegPenze(aktualisTet))
                {
                    jatekos.Penz -= aktualisTet;
                    aktualisTet *= 2;
                    sajatLapok.Add(KartyaHuzas());
                    Console.WriteLine("Split! Tét megduplázva, kaptál egy új lapot.");
                }
            }


            int sajatVegso = PontSzamilas(sajatLapok);
            Console.WriteLine($"\nSaját végleges pontszámod: {sajatVegso}");

            if (sajatVegso > 21)
            {
                Console.WriteLine("BESOKALLTÁL! A ház nyert.");
            }
            else
            {

                Console.WriteLine("\n--- Osztó köre ---");
                int osztoPont = PontSzamilas(osztoLapok);

                while (osztoPont < 17)
                {
                    Console.WriteLine($"Osztó lapjai: {string.Join(", ", osztoLapok)} (Összeg: {osztoPont})");
                    System.Threading.Thread.Sleep(800);
                    osztoLapok.Add(KartyaHuzas());
                    osztoPont = PontSzamilas(osztoLapok);
                }

                Console.WriteLine($"Osztó végső keze: {string.Join(", ", osztoLapok)} (Összeg: {osztoPont})");

                if (osztoPont > 21 || sajatVegso > osztoPont)
                {
                    Console.WriteLine("GRATULÁLOK, NYERTÉL!");
                    jatekos.Penz += aktualisTet * 2;
                }
                else if (sajatVegso == osztoPont)
                {
                    Console.WriteLine("DÖNTETLEN (PUSH)!");
                    jatekos.Penz += aktualisTet;
                }
                else
                {
                    Console.WriteLine("AZ OSZTÓ NYERT!");
                }
            }

            Console.WriteLine($"\nÚj egyenleged: {jatekos.Penz} Ft");
            Console.WriteLine("Nyomj meg egy gombot a kilépéshez...");
            Console.ReadKey();
        }

        private int PontSzamilas(List<Kartya> lapok)
        {
            int osszeg = 0;
            int aszok = 0;
            foreach (var k in lapok)
            {
                osszeg += k.Ertek;
                if (k.Nev == "Ász") aszok++;
            }
            while (osszeg > 21 && aszok > 0)
            {
                osszeg -= 10;
                aszok--;
            }
            return osszeg;
        }
    }
}