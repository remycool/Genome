using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_DAL
{
    public enum role
    {
        orchestrateur = 1,
        noeud = 2
    }

    public enum etat
    {
        connected = 4,
        workInProgress = 5,
        workDone = 6,
        notConnected = 7
    }

    class Program
    {
        const string BDD = "MYSQL";
        static void Main(string[] args)
        {
            using (ClusterDAL dal = new ClusterDAL(BDD))
            {
                Console.WriteLine("Récupération des IP du cluster");

                //etat du cluster
                Console.WriteLine(dal.GetClusterRegistry());
                //Récuper les IP

                List<IPAddress> liste = dal.GetAllNodeIPs();

                //si la liste est vide
                if (liste.Count == 0)
                    Console.WriteLine("Aucun noeud connecté");
                else
                    foreach (IPAddress ip in liste)
                        Console.WriteLine($"{ip}\n");
                Console.ReadKey();

                //Mettre à jour le registre
                dal.UpdateCluster("192.168.0.25", etat.connected, role.orchestrateur);
                //etat du cluster
                Console.WriteLine(dal.GetClusterRegistry());
                Console.ReadKey();
            }

            
        }
    }
}
