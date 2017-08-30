using Cluster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class Resultat
    {
        public string IpNoeud { get; set; }
        public int Id { get; set; }
        public int Valeur { get; set; }
        public double ValeurPrcent { get; set; }
        public string ValeurString { get; set; }
        public long TempsExecution { get; set; }
        public string Erreur { get; set; }

        public Resultat()
        {

        }

        public static Resultat operator +(Resultat a, Resultat b)
        {
            a.Valeur += b.Valeur;
            return a;
           // return new Resultat() {  Valeur = a.Valeur + b.Valeur };
        }

        public override string ToString()
        {
            return $"\n>>> Opération {Id} effectué par {IpNoeud} --> Résultat du comptage : {Valeur} ";
        }
    }
}
