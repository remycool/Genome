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
            string sql = @"SELECT * FROM cluster WHERE type_noeud = 1 and etat_noeud <> 7 ";
            using (DbDataReader reader = Get(sql, null))
            {
                while (reader.Read())
                {
                    string adresse = Convert.ToString(reader[1]);
                    liste.Add(IPAddress.Parse(adresse));
                }
            }


            return liste;
        }

        public string GetClusterRegistry()
        {
            string result = string.Empty;
            string sql = @"SELECT * FROM cluster_view ";

            using (DbDataReader reader = Get(sql, null))
            {
                while (reader.Read())
                {
                    string ip = Convert.ToString(reader[0]);
                    string etat = Convert.ToString(reader[1]);
                    string role = Convert.ToString(reader[2]);

                    result += $"\n{role} {ip} {etat}\n";
                }
            }

            return result;
        }

        /// <summary>
        /// Appelle une fonction stockée de la bdd
        /// </summary>
        /// <param name="adresseIP"> l'adresse IP au format chaine</param>
        /// <param name="etat">l'id de l'état</param>
        /// <param name="role">l'id du role</param>
        /// <returns>le code retour</returns>
        public int UpdateCluster(string adresseIP, etat etat, role role)
        {
            int result = 0;

            if (!isCorrectIp(adresseIP))
                throw new Exception("L'adresse IP n'est pas au format valide");

            string sql = "select cluster_db.maj_cluster(@1, @2, @3)";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@1", adresseIP }, { "@2", (int)etat }, { "@3", (int)role } };

            using (DbDataReader reader = Get(sql, parameters))
            {
                while (reader.Read())
                {
                    result = Convert.ToInt32(reader[0]);
                }
            }
           
            return result;
        }

        private bool isCorrectIp(string adresseIp)
        {
            IPAddress checkedIp;
            return IPAddress.TryParse(adresseIp, out checkedIp);
        }
    }
}
