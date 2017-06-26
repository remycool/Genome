﻿using Cluster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class ResultatCalcul:IClusterizable
    {
        public int Resultat { get; set; }

        public long TempsExecution { get; set; }
    }
}
