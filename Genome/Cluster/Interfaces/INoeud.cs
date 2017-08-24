using Cluster.Classes;
using System;

namespace Cluster.Interfaces
{
    public interface INoeud : IDisposable
    {
        Operation GetResultat();

        void Initialize();
        //Operation Recevoir();
        void Envoyer(Operation op);
        void RepartirCalcul(string file,string methode);

    }
}
