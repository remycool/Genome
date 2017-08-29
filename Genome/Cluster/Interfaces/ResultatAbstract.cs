using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Interfaces
{
    public abstract class ResultatAbstract
    {
        public string IpNoeud;
        public int Id;

        public ResultatAbstract(int id , string ip)
        {
            IpNoeud = ip;
            Id = id;
        }
    } 
}
