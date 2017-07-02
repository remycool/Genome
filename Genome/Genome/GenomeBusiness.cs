using Cluster.Classes;
using Cluster.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Genome.GenomeBusiness
{
    public class GenomeBusiness:IClusterizable
    {

        Operation IClusterizable.Calcul1(string chunk)
        {
            char charToCount = 'C';
            Stopwatch sw = Stopwatch.StartNew();
            //opération
            int count = 0;
            for (int i = 0; i < chunk.Length; i++)
            {
                if (chunk[i] == charToCount)
                    count++;
            }
            //On arrête le chrono
            sw.Stop();

            return new Operation { Resultat = count, TempsExecution = sw.ElapsedMilliseconds };
        }
    }
}
