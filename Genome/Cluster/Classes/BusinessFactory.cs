using System;
using System.Collections.Generic;
using Cluster.Interfaces;

namespace Cluster.Classes
{
    
    public class BusinessFactory:IBusinessFactory
    {
        private IClusterizableBusiness _clusterizableBusiness;

        public BusinessFactory(IClusterizableBusiness business)
        {
            _clusterizableBusiness = business;
        }

        public IResultat GetCalcul(string chunckFile)
        {
            return _clusterizableBusiness.Calculer(chunckFile);
        }

    }
}
