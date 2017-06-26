using System;
using Cluster.Interfaces;

namespace Cluster
{
    public class Calcul<T>:IClusterizable
    {
        public string Type { get; set; }
        public T Param { get; set; }
    }
}
