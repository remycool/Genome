using Cluster.Classes;
using System;

namespace Cluster.Interfaces
{
    public interface INoeud : IDisposable
    {
        void Initialize();
        Operation Attente();
        Operation Envoyer(Operation op);
        void RepartirCalcul(string file,string methode);
    }
}
