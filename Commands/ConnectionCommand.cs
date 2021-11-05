using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using sharp_websocketcp.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace sharp_websocketcp.Commands
{
    [Command("bind", Description = "绑定指定websocket到指定的TCP端")]
    public class ConnectionCommand : ICommand
    {
        [CommandParameter(0, Name = "local", Description = "本地监听地址")]
        public string LocalAddress { get; set; } = "";


        [CommandParameter(1, Name = "remote", Description = "远程TCP地址")]
        public string RemoteAddress { get; set; } = "";

        public ValueTask ExecuteAsync(IConsole console)
        {
            var wssv = new WebSocketServer($"ws://{this.LocalAddress}");

            wssv.AddWebSocketService<TcpServerHandler>($"/tcpserver");

            wssv.Start();

            Console.WriteLine("websocket服务已启动.....");

            Console.ReadKey(true);

            wssv.Stop();

            return default;
        }
    }
}
