using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Protocole
{
    public class UdpClientCluster:UdpClient
    {
        public UdpClientCluster(int port) : base(port )
        {

        }

        /// <summary>
        /// Donne l'information le client Udp est actif
        /// </summary>
        public new bool Active
        {
            get { return base.Active; }
        }
    }
}
