using CliFx;
using Microsoft.Extensions.DependencyInjection;
using sharp_websocketcp.Commands;
using sharp_websocketcp.Handler;
using System;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace sharp_websocketcp
{

    class Program
    {
        public static async Task<int> Main() =>
           await new CliApplicationBuilder()
               .AddCommandsFromThisAssembly()
               .UseTypeActivator(GetServiceProvider().GetRequiredService)
               .Build()
               .RunAsync();

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddTransient<ConnectionCommand>();

            return services.BuildServiceProvider();
        }

    }
}
