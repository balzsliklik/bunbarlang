using System;

namespace bunbarlang
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Jatekos jatekos = new Jatekos(10000);


            Kartya21 blackjack = new Kartya21();
            Roulette rulett = new Roulette();
            Penzfeldobas feldobas = new Penzfeldobas();

            bool fut = true;
            while (fut)
            {
                Console.Clear();
                Console.WriteLine("*************************************");
                Console.WriteLine($"   KASZINÓ - Egyenleg: {jatekos.Penz} Ft");
                Console.WriteLine("*************************************");
                Console.WriteLine("1. Blackjack (21)");
                Console.WriteLine("2. Rulett");
                Console.WriteLine("3. Pénzfeldobás");
                Console.WriteLine("0. Kilépés");
                Console.Write("\nVálasztás: ");

                string valasztas = Console.ReadLine();

                switch (valasztas)
                {
                    case "1":
                        blackjack.Jatszik(jatekos);
                        break;
                    case "2":
                        rulett.Jatszik(jatekos);
                        break;
                    case "3":
                        feldobas.Jatszik(jatekos);
                        break;
                    case "0":
                        fut = false;
                        break;
                    default:
                        Console.WriteLine("Nincs ilyen opció!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("cs");
            
        }
    }
}