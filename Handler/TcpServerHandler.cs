using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace sharp_websocketcp.Handler
{
    public class TcpServerHandler : WebSocketBehavior
    {
        private bool tcpServerConnected = false;

        private readonly int tcpServerPort = 6666;

        private readonly string tcpServerIp = "127.0.0.1";

        private TcpClient tcpClient;

        protected override void OnMessage(MessageEventArgs e)
        {
            this.ConnectTcpServer(e);
        }
        private void ConnectTcpServer(MessageEventArgs e)
        {
            if (tcpClient == null)
            {
                tcpClient = new TcpClient();

            }

            if (!this.tcpClient.Connected)
            {
                tcpClient.Connect(IPAddress.Parse(this.tcpServerIp), this.tcpServerPort);

                Console.WriteLine($"{this.tcpServerIp}：{this.tcpServerPort}连接已经建立");
            }

            NetworkStream ns = tcpClient.GetStream();

            if (ns.CanWrite)
            {
                var msgByte = e.RawData;

                Console.WriteLine("发送了消息：" + Encoding.UTF8.GetString(e.RawData));

                ns.Write(msgByte, 0, msgByte.Length);
            }


            if (!tcpServerConnected)
            {
                tcpServerConnected = true;

                var thread = new Thread(o => StartReceiveTcpMsg((TcpClient)o));

                thread.Start(tcpClient);
            }
        }
        void StartReceiveTcpMsg(TcpClient client)
        {
            Console.WriteLine("开始读取TCP服务消息.....");

            var ns = client.GetStream();

            byte[] receivedBytes = new byte[client.ReceiveBufferSize];

            while ((_ = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                base.Send(receivedBytes);

                Console.WriteLine("收到了TcpServer消息：" + Encoding.UTF8.GetString(receivedBytes));

                //重置消息
                receivedBytes = new byte[client.ReceiveBufferSize];
            }
        }
    }
}
