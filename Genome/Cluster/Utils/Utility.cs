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
        //public static string Compress(this string tocompress)
        //{
        //    Console.WriteLine($"Taille avant compression : {tocompress.Length}");
        //    byte[] stringToArray = Encoding.UTF8.GetBytes(tocompress);
        //    string compressedString = string.Empty;
        //    using (MemoryStream ms = new MemoryStream())
        //    using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
        //    {
        //        zip.Write(stringToArray, 0, stringToArray.Length);
        //        zip.Close();
        //        ms.Position = 0;

        //        byte[] compressed = new byte[ms.Length];
        //        ms.Read(compressed, 0, compressed.Length);
        //        ms.Close();
        //        ms.Dispose();
        //        byte[] gzBuffer = new byte[compressed.Length + 4];
        //        Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
        //        Buffer.BlockCopy(BitConverter.GetBytes(tocompress.Length), 0, gzBuffer, 0, 4);
        //        compressedString = Convert.ToBase64String(gzBuffer);
        //    }

        //    Console.WriteLine($"Taille après compression : {compressedString.Length}");
        //    return compressedString;
        //}

        ///// <summary>
        ///// Methode d'extension permettant de decompresser un tableau de byte
        ///// </summary>
        ///// <param name="tocompress"></param>
        ///// <returns>Un tableau de byte décompressé</returns>
        //public static string Decompress(this string toDecompress)
        //{
        //    Console.WriteLine($"Taille avant decompression : {toDecompress.Length}");
        //    byte[] stringToArray = Convert.FromBase64String(toDecompress);

        //    MemoryStream ms = new MemoryStream();
        //    int msgLength = BitConverter.ToInt32(stringToArray, 0);
        //    ms.Write(stringToArray, 4, stringToArray.Length - 4);

        //    byte[] buffer = new byte[msgLength];

        //    ms.Position = 0;
        //    GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
        //    zip.Read(buffer, 0, buffer.Length);
        //    string stringDecompressed = Encoding.UTF8.GetString(buffer);
        //    Console.WriteLine($"Taille après décompression : {stringDecompressed.Length}");
        //    return stringDecompressed;
        //}

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
                   // compressionStream.Flush();
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
                Console.WriteLine(serializedObject);
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
        /// <returns>Un objet de type IClusterizable </returns>
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
