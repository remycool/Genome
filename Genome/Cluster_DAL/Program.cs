using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_DAL
{
   

    class Program
    {
        
        static void Main(string[] args)
        {
            using (ClusterDAL dal = new ClusterDAL())
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
                dal.UpdateCluster("192.168.0.21",4,1);
                //etat du cluster
                Console.WriteLine(dal.GetClusterRegistry());
                Console.ReadKey();
            }

            
        }
    }
}
