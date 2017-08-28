using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public interface IDALFactory
    {
        List<IPAddress> GetAllNodeIPs();
        string GetClusterView();
        int UpdateNode(string adresseIP, int etat, int role);
        string GetOrchestrateurIP();
        void Dispose();
    }
}
