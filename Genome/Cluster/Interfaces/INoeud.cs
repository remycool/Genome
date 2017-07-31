using Cluster.Classes;
using System;

namespace Cluster.Interfaces
{
    public interface INoeud : IDisposable
    {
        void Initialize();
        Operation Attente();
        Operation Envoyer(Operation operation);
        int Map(string chunck);
        //test
        int CountChars(string chunk, char charToCount);
    }
}
