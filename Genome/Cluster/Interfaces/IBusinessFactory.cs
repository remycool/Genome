using System.Collections.Generic;

namespace Cluster.Classes
{
    public interface IBusinessFactory
    {
        Resultat GetCalcul1(string fileChunck);
        Resultat GetCalcul2(string fileChunck);
    }
}