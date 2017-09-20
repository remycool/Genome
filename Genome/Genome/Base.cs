using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genome
{
    public class Base
    {
        public int NbBaseA { get; set; }
        public int NbBaseT { get; set; }
        public int NbBaseG { get; set; }
        public int NbBaseC { get; set; }
        public int NbBaseInconnue { get; set; }


        public static Base operator +(Base a, Base b)
        {
            a.NbBaseA += b.NbBaseA;
            a.NbBaseT += b.NbBaseT;
            a.NbBaseG += b.NbBaseG;
            a.NbBaseC += b.NbBaseC;
            a.NbBaseInconnue += b.NbBaseInconnue;

            return a;
            // return new Resultat() {  Valeur = a.Valeur + b.Valeur };
        }
    }
}
