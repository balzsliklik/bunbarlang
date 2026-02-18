using System;

namespace bunbarlang
{
    internal class Jatekos
    {
        private int penz;

        public Jatekos(int kezdoPenz)
        {
            this.penz = kezdoPenz;
        }

        public int Penz { get => penz; set => penz = value; }

        public bool VanElegPenze(int osszeg)
        {
            return penz >= osszeg;
        }
    }
}