using Cluster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class Resultat<T>:IResultat
    {
        public string IpNoeud { get; set; }
        public int Id { get; set; }
        public T Valeur { get; set; }
        public long TempsExecution { get; set; }
        public string Erreur { get; set; }
        public Etat_noeud Etat { get; set; }
        public bool HasValue { get; set; }

        public Resultat()
        {
        }

        public static Resultat<T> operator +(Resultat<T> res1,Resultat<T> res2)
        {
            dynamic a = res1.Valeur;
            dynamic b = res2.Valeur;
            res2.Valeur = a + b;

            return res2;
        }

        public override string ToString()
        {
            return $"\n>>> Opération {Id} effectué par {IpNoeud} ";
        }
    }
}
