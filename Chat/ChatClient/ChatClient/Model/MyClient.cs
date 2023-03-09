using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.Model
{
    public class MyClient
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public string Message { get; set; }
        private Socket clientSocket;

        public MyClient()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        public void Connect(string ip, int port)
        {
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

        }

        public async Task<string> Receive()
        {

            await Task.Run(() =>
            {
                var data = new byte[1024];
                int bytes = clientSocket.Receive(data);
                Message = Encoding.ASCII.GetString(data, 0, bytes);

           });
            return Message;

        }




        public void Send(string text)
        {
            var data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            clientSocket.Send(data);

        }


    }
}
