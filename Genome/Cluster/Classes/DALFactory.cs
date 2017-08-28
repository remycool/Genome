using Cluster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Cluster.Classes
{
   public class DALFactory:IDALFactory,IDisposable
    {
        private IClusterizableDAL _clusterizableDal;

        public DALFactory(IClusterizableDAL dal)
        {
            _clusterizableDal = dal;
        }

        public void Dispose()
        {
            if (_clusterizableDal != null)
                _clusterizableDal.Dispose();
        }

        public List<IPAddress> GetAllNodeIPs()
        {
            return _clusterizableDal.GetAllNodeIPs();
        }

        public string GetClusterView()
        {
            return _clusterizableDal.GetClusterRegistry();
        }

        public string GetOrchestrateurIP()
        {
            return _clusterizableDal.GetOrchestrateurIp();
        }

        public int UpdateNode(string adresseIP, int etat, int role)
        {
            return _clusterizableDal.UpdateCluster(adresseIP,etat,role);
        }
    }
}
