﻿using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Interfaces
{
    public interface IClusterizableBusiness
    {
        IResultat Calculer(string chunkFile);
        
    }
}
