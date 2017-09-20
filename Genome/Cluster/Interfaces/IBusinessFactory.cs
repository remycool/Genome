using Cluster.Interfaces;
using System.Collections.Generic;

namespace Cluster.Classes
{
    public interface IBusinessFactory
    {
        IResultat GetCalcul(string fileChunck);
    }
}