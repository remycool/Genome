using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Events
{
    public class ResultatEventArgs
    {
        public Resultat Op;
        public ResultatEventArgs(Resultat r) { Op = r; }
    }
}
