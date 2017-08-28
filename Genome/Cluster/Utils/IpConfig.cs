using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Utils
{
    public class IpConfig
    {
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
    }
}
