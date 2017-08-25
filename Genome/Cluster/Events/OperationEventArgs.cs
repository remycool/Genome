using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Events
{
    public class ReceptionEventArgs<T>
    {
        public T Op;
        public ReceptionEventArgs(T o) { Op = o; }
    }
}
