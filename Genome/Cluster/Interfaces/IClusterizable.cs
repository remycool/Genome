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
        Operation Calcul1(string chunkFile);
        Operation Calcul2(string chunkfile);
    }
}
