using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Servak.Model
{
    public class MyServer
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public ObjectColor obColor { get; set; }
        public string Result { get; set; }

        private Socket serverSocket;
        private Socket clientSocket;
        public MyServer()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(string ip, int port)
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(10);
            
        }
    

    public async Task<ObjectColor> Receive()
    {
   
            await Task.Run(() =>
            {
                if (clientSocket == null)
                {
                    clientSocket = serverSocket.Accept();
                }
                var buffer = new byte[1024];
                clientSocket.Receive(buffer);
                obColor = ObjectColor.Deserialize(Encoding.ASCII.GetString(buffer));
            });
            return obColor;
    }
    public void Send(string text)
        {
            var buffer = Encoding.ASCII.GetBytes(text);
            clientSocket.Send(buffer);
        }
        public void Stop()
        {
            //serverSocket.Shutdown(SocketShutdown.Both);
            //serverSocket.Close();
        }

    }
}
