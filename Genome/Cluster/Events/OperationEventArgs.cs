﻿using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Events
{
    public class OperationEventArgs
    {
        public Operation Op;
        public OperationEventArgs(Operation o) { Op = o; }
    }
}
