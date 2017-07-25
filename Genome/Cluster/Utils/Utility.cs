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
    public static class Utility
    {
        public static string Decompress(this string input)
        {
            byte[] compressed = Convert.FromBase64String(input);
            byte[] decompressed = Decompress(compressed);
            return Encoding.UTF8.GetString(decompressed);
        }

        public static string Compress(this string input)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(input);
            byte[] compressed = Compress(encoded);
            return Convert.ToBase64String(compressed);
        }

        public static byte[] Decompress( byte[] input)
        {
            using (MemoryStream source = new MemoryStream(input))
            {
                byte[] lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);

                int length = BitConverter.ToInt32(lengthBytes, 0);
                using (var decompressionStream = new GZipStream(source, CompressionMode.Decompress))
                {
                    byte[] result = new byte[length];
                    decompressionStream.Read(result, 0, length);
                    return result;
                }
            }
        }

        public static byte[] Compress( byte[] input)
        {
            using (MemoryStream result = new MemoryStream())
            {
                byte[] lengthBytes = BitConverter.GetBytes(input.Length);
                result.Write(lengthBytes, 0, 4);

                using (GZipStream compressionStream = new GZipStream(result,CompressionMode.Compress))
                {
                    compressionStream.Write(input, 0, input.Length);
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// Obtient l'adresse IP de la machine 
        /// </summary>
        /// <returns>Un objet IPAdress</returns>
        public static IPAddress GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Impossible d'obtenir l'IP de la machine");
        }

        /// <summary>
        /// Sérialise l'objet passé en paramètre et le transforme en tableau de bytes 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>L'objet sous forme de tableau</returns>
        public static byte[] Serialize(Operation c)
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
        public static Operation Deserialize(string serializedObject)
        {

            Operation result = null;
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = 30000000 };
            try
            {
                result = js.Deserialize<Operation>(serializedObject);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }

            return result;
        }

    }
}
