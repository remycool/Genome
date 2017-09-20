using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Events
{
    public class ReceptionEventArgs
    {
        public TcpClient Noeud;
        public ReceptionEventArgs(TcpClient n) { Noeud = n; }
    }
}
