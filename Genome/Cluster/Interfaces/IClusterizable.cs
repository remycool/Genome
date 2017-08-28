using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Interfaces
{
    public interface IClusterizableBusiness
    {
        Resultat Calcul1(string chunkFile);
        Resultat Calcul2(string chunkfile);
        Resultat Calcul3(List<string> chunkfile);
    }
}
