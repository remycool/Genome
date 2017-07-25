using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Interfaces
{
    public interface IClusterizableDAL
    {
        List<IPAddress> GetAllNodeIPs();
        string GetClusterRegistry();
        int UpdateCluster(string adresseIP, int etat, int  role);
        string GetOrchestrateurIp();
        void Dispose();
    }
}
