using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Events
{
    public class ResultatEventArgs<T>
    {
        public Resultat<T> Result;
        public ResultatEventArgs(Resultat<T> r) { Result = r; }
    }
}
