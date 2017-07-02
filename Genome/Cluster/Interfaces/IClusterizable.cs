using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Interfaces
{
    public interface IClusterizable
    {
        Operation Calcul1(string chunkFile);
    }
}
