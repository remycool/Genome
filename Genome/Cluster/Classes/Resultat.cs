﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class Resultat
    {
        public string IpNoeud { get; set; }
        public int Valeur { get; set; }
        public long TempsExecution { get; set; }

        public static Resultat operator +(Resultat a, Resultat b)
        {
            a.Valeur += b.Valeur;
            return a;
           // return new Resultat() {  Valeur = a.Valeur + b.Valeur };
        }

        public override string ToString()
        {
            return $">>> Résultat : {Valeur} effectué par {IpNoeud} en {TempsExecution} ms";
        }
    }
}