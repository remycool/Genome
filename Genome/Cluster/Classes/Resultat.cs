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
        public Etat_noeud Etat { get; set; }
        public bool HasValue { get; set; }

        public Resultat()
        {
        }

        public static Resultat operator +(Resultat a, Resultat b)
        {
            a.Valeur += b.Valeur;
            a.TempsExecution += b.TempsExecution;
            a.ValeurPrcent += b.ValeurPrcent;
            a.ValeurString += b.ValeurString;
            return a;
    }

        public override string ToString()
        {
            return $"\n>>> Opération {Id} effectué par {IpNoeud} --> Résultat du comptage : {Valeur} {ValeurPrcent} {ValeurString}";
        }
    }
}
