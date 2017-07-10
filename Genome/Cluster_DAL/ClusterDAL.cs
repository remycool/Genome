using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_DAL
{
    public class ClusterDAL : DAL
    {
        public ClusterDAL(string nomBdd) : base(nomBdd)
        {
        }

        /// <summary>
        /// Selectionne tous les enregistrements de la Bdd
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Une liste d'adresse IP</returns>
        public List<IPAddress> GetAllNodeIPs()
        {
            List<IPAddress> liste = new List<IPAddress>();
            string sql = @"SELECT * FROM cluster WHERE type_noeud = 1";
           
            DbDataReader reader = Get(sql, null);
            while (reader.Read())
            {
                string adresse = Convert.ToString(reader[1]);
                liste.Add(IPAddress.Parse(adresse));
            }
            return liste;
        }

        /// <summary>
        /// Appelle une fonction stockée de la bdd
        /// </summary>
        /// <param name="adresseIP"> l'adresse IP au format chaine</param>
        /// <param name="etat">l'id de l'état</param>
        /// <param name="role">l'id du role</param>
        /// <returns>le code retour</returns>
        public int UpdateCluster(string adresseIP,int etat ,int role)
        {
            int result = 0;
            string sql = @"select cluster_db.maj_cluster(@1, @2, @3)";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@1", adresseIP }, { "@2", etat }, { "@3", role } };

            DbDataReader reader = Get(sql, null);
            while (reader.Read())
            {
                result = Convert.ToInt32(reader[0]);
            }
            return result;
         }
    }
}
