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

        public Resultat GetCalcul1(string chunckFile)
        {
            return _clusterizableBusiness.Calcul1(chunckFile);
        }

        public Resultat GetCalcul2(string chunckFile)
        {
            return _clusterizableBusiness.Calcul2(chunckFile);
        }
    }
}
