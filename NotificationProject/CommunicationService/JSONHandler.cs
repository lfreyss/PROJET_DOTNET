﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class JSONHandler
    {
        public static JObject stringToJson(string chaine)
        {
            JObject message = JObject.Parse(chaine);
            return message;
        }

        public static void interpretation(JObject json)
        {
            string type = (string)json["type"];
            if (type != "")
            {
                int port;
                string[] adressBuffer = ((string)(json["conn"])).Split('@');
                IPAddress ipAddress = IPAddress.Parse(adressBuffer[0]);
                port = Int32.Parse(adressBuffer[1]);
                string author = (string)json["author"];

                if (type.ToLower() == "connect")
                {
                    Console.WriteLine("Connection"); 
                    /*
                     * - Méthode de connexion au serveur -
                     * TcpListener client = new TcpListener(ipAddress, port);
                     * sServer = client.AcceptSocket();
                     * Console.WriteLine("Connection de l'appareil " +author+"("+sServer.RemoteEndPoint+") acceptée.");
                     */
                    Console.WriteLine("L'appareil " + author + "(" + ipAddress.ToString() + ":" + port.ToString() + ") souhaite se connecter.");
                }

                else if (type.ToLower() == "notification")
                {
                    Console.WriteLine("Notification");
                    IList<string> allObject = json["object"].Select(t => (string)t).ToList();
                    string application = allObject[0];
                    string message = allObject[1];
                    DateTime dateNotif = DateTime.Parse(allObject[2]);

                    //Démonstration utilisation des objets obtenus depuis le JSON
                    Console.WriteLine("L'application " + application + " a reçu le message suivant: '" + message + "' depuis l'appareil de " + author + " à " + dateNotif + ".");
                }
            }
            else
                Console.WriteLine("Message invalide.");

        }
    }
}
