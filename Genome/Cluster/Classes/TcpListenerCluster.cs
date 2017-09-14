using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class TcpListenerCluster : TcpListener
    {
        /// <summary>
        /// Initialise une nouvelle instance avec le point de terminaison passé en paramètre
        /// </summary>
        /// <param name="localEP"> </exception>
        public TcpListenerCluster(IPEndPoint localEP) : base(localEP)
        {
        }

        /// <summary>
        ///Initialise une nouvelle instance en précisant une adresse IP et un port de communication
        /// </summary>
        /// <param name="localaddr">An <see cref="T:System.Net.IPAddress"/> that represents the local IP address. </param><param name="port">The port on which to listen for incoming connection attempts. </param><exception cref="T:System.ArgumentNullException"><paramref name="localaddr"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="port"/> is not between <see cref="F:System.Net.IPEndPoint.MinPort"/> and <see cref="F:System.Net.IPEndPoint.MaxPort"/>. </exception>
        public TcpListenerCluster(IPAddress localaddr, int port) : base(localaddr, port)
        {
        }

        /// <summary>
        /// Donne l'information si l'écoute est active ou pas
        /// </summary>
        public new bool Active
        {
            get { return base.Active; }
        }
    }
}
