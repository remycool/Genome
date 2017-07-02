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
        /// <summary>
        /// Methode d'extension permettant de compresser un tableau de byte
        /// </summary>
        /// <param name="tocompress"></param>
        /// <returns>Un tableau de byte compressé</returns>
        public static byte[] Compress(this byte[] tocompress)
        {
            Console.WriteLine($"Taille avant compression : {tocompress.Length}");
            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(tocompress, 0, tocompress.Length);
            zip.Close();
            ms.Position = 0;

            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(tocompress.Length), 0, gzBuffer, 0, 4);

            Console.WriteLine($"Taille après compression : {gzBuffer.Length}");
            return gzBuffer;
        }


        /// <summary>
        /// Methode d'extension permettant de decompresser un tableau de byte
        /// </summary>
        /// <param name="tocompress"></param>
        /// <returns>Un tableau de byte décompressé</returns>
        public static byte[] Decompress(this byte[] toDecompress)
        {
            Console.WriteLine($"Taille avant decompression : {toDecompress.Length}");
            MemoryStream ms = new MemoryStream();
            int msgLength = BitConverter.ToInt32(toDecompress, 0);
            ms.Write(toDecompress, 4, toDecompress.Length - 4);

            byte[] buffer = new byte[msgLength];

            ms.Position = 0;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            zip.Read(buffer, 0, buffer.Length);

            Console.WriteLine($"Taille après décompression : {buffer.Length}");
            return buffer;
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
        public static byte[] Serialize(Operation  c ){
            string serializedObject = null;
            byte[] b = null;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                serializedObject = js.Serialize(c);
                Console.WriteLine(serializedObject);
                b = Encoding.UTF8.GetBytes(serializedObject);

            }
            catch(Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }

            return b;
        }

        /// <summary>
        /// Désérialyse la chaine représentant l'objet
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns>Un objet de type IClusterizable </returns>
        public static Operation Deserialize(string serializedObject) {

            Operation result = null;
            JavaScriptSerializer js = new JavaScriptSerializer() {MaxJsonLength = 20971520 };
            try
            {
                result = js.Deserialize<Operation>(serializedObject);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message +  ex.StackTrace);
            }
            
            return result;
        }
    }
}
