using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Servak.Model;

namespace Servak
{
    public class myClient
    {
        public string Ip { get; set; }
        public ObjectColor obColor { get; set; }
        public int Port { get; set; }
        private Socket clientSocket;
        public string Result { get; set;}
        public myClient()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(string ip, int port)
        {
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        }

        public void Receive()
        {
            var buffer = new byte[1024];
            clientSocket.Receive(buffer);
            Result = Encoding.ASCII.GetString(buffer);
        }
        public void Send(ObjectColor obColor)
        {
            var text = ObjectColor.Serialize(obColor);
            var buffer = Encoding.ASCII.GetBytes(text);
            clientSocket.Send(buffer);

        }
        public void Stop()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
