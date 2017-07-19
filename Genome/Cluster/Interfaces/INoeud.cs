using Cluster.Classes;

namespace Cluster.Interfaces
{
    public interface INoeud
    {
         void Initialize();
         void Close();
        void AttenteCalcul();
        Operation EnvoyerCalcul(Operation operation);
    }
}
