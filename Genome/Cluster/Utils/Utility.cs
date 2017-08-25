using Cluster.Classes;
using Cluster.Interfaces;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace Cluster.Utils
{
    public class Utility<T>
    {
        /// <summary>
        /// Sérialise l'objet passé en paramètre et le transforme en tableau de bytes 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>L'objet sous forme de tableau</returns>
        public static byte[] Serialize(T c)
        {
            string serializedObject = null;
            byte[] b = null;
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = 30000000 };
            try
            {
                serializedObject = js.Serialize(c);
               // Console.WriteLine(serializedObject);
                b = Encoding.UTF8.GetBytes(serializedObject);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }

            return b;
        }

        /// <summary>
        /// Désérialyse la chaine représentant l'objet
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns>Un objet de type IClusterizableBusiness </returns>
        public static T Deserialize(string serializedObject)
        {

            T result = default(T);
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = 30000000 };
            try
            {
                result = js.Deserialize<T>(serializedObject);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }

            return result;
        }

    }
}
