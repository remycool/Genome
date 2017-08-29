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

        public Resultat GetCalcul1(string chunckFile)
        {
            return _clusterizableBusiness.Calcul1(chunckFile);
        }

        public Resultat GetCalcul10(string fileChunck)
        {
            return _clusterizableBusiness.Calcul10(fileChunck);
        }

        public Resultat GetCalcul2(string chunckFile)
        {
            return _clusterizableBusiness.Calcul2(chunckFile);
        }

        public Resultat GetCalcul3(string chunckFile)
        {
            return _clusterizableBusiness.Calcul3(chunckFile);
        }
        public Resultat GetCalcul4(string chunckFile)
        {
            return _clusterizableBusiness.Calcul4(chunckFile);
        }

        public Resultat GetCalcul5(string fileChunck)
        {
            return _clusterizableBusiness.Calcul5(fileChunck);
        }

        public Resultat GetCalcul6(string fileChunck)
        {
            return _clusterizableBusiness.Calcul6(fileChunck);
        }

        public Resultat GetCalcul7(string fileChunck)
        {
            return _clusterizableBusiness.Calcul7(fileChunck);
        }

        public Resultat GetCalcul8(string fileChunck)
        {
            return _clusterizableBusiness.Calcul8(fileChunck);
        }

        public Resultat GetCalcul9(string fileChunck)
        {
            return _clusterizableBusiness.Calcul9(fileChunck);
        }
    }
}
