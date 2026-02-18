using System;

namespace bunbarlang
{
    internal class Kartya
    {
        private string szin;
        private string nev;
        private int ertek;

        public Kartya(string szin, string nev, int ertek)
        {
            this.szin = szin;
            this.nev = nev;
            this.ertek = ertek;
        }

        public string Szin { get => szin; }
        public string Nev { get => nev; }
        public int Ertek { get => ertek; set => ertek = value; }

        public override string ToString()
        {
            return $"{szin} {nev}";
        }
    }
}