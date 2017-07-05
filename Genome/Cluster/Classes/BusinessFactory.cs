using Cluster.Interfaces;

namespace Cluster.Classes
{
    public class BusinessFactory:IBusinessFactory
    {
        private IClusterizable _clusterizableBusiness;

        public BusinessFactory(IClusterizable business)
        {
            _clusterizableBusiness = business;
        }

        public Operation GetCalcul1(string chunckFile)
        {
            return _clusterizableBusiness.Calcul1(chunckFile);
        }

        public Operation GetCalcul2(string chunckFile)
        {
            return _clusterizableBusiness.Calcul2(chunckFile);
        }
    }
}
