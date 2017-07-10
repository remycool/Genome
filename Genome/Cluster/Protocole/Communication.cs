﻿using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cluster.Protocole
{
    public class Communication
    {
        private static Communication instance;
        private static object syncRoot = new object();
        public IPAddress AdresseIpLocale { get; set; }
        public const int PORT = 8888;



        private Communication()
        {


        }

        public static Communication Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Communication();
                    }
                }

                return instance;
            }
        }


        public void Envoyer(IPAddress remote, Operation obj)
        {
            IPEndPoint remoteEP = new IPEndPoint(remote, PORT);
            TcpClient local = new TcpClient();
            local.Connect(remoteEP);
            try
            {
                byte[] ba = Utility.Serialize(obj);
                using (NetworkStream ns = local.GetStream())
                {
                    ns.Write(ba, 0, ba.Length);
                };
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }
            finally
            {
                local.Close();
            }
        }

        public Operation Recevoir(IPAddress AdresseIpLocale)
        {
            Operation obj = null;

            //Initialisation du listener
            TcpListener localListener = new TcpListener(AdresseIpLocale, PORT);
            //Débuter l'écoute
            localListener.Start();


            try
            {
                using (TcpClient remote = localListener.AcceptTcpClient())
                using (NetworkStream ns = remote.GetStream())
                {
                    int i = 0;
                    byte[] remoteData = new byte[1024];
                    string data = string.Empty;
                    //Lecture du flux
                    if (ns.CanRead)
                    {
                        while ((i = ns.Read(remoteData, 0, remoteData.Length)) != 0)
                        {
                            data += Encoding.UTF8.GetString(remoteData, 0, i);
                        }
                        obj = Utility.Deserialize(data);
                    }
                }

                //Décompresser le fichier 
                string unzipFile = obj.Param.Decompress();
                obj.Param = string.Empty;
                obj.Param = unzipFile;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }
            finally
            {
                localListener.Stop();
            }

            return obj;
        }

    }
}