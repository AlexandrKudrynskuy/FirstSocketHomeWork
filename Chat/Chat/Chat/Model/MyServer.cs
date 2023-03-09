using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace ServerChat.Model
{
    public class MyServer
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public string Message { get; set; }
        private readonly Socket serverSocket;
        private Socket clientSocket;

        public MyServer()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async void Start(string ip, int port)
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(10);
          
          
        }

        public async Task<string> ReceiveAsync()
        {
            await Task.Run(() =>
            {
                if (clientSocket == null)
                {
                    clientSocket = serverSocket.Accept();
                }
                var data = new byte[1024];
                int bytes = clientSocket.Receive(data);
                Message = Encoding.ASCII.GetString(data, 0, bytes);
            });
           return Message;
        }

        public void Send(string text)
        { 
            var data = Encoding.ASCII.GetBytes(text);
            clientSocket.Send(data);

        }


    }
}
