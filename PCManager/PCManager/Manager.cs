using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AssaultCubeExternal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PCManager
{
    public class Manager
    {
        private IHost _restApiHost;
        private readonly AssaultCubeMain _assaultCubeExternal = new AssaultCubeMain();

        public async Task Init(string[] args)
        {
            // Start the REST API
            _restApiHost = CreateRestApiHost(args);
            await _restApiHost.StartAsync();
        }

        private IHost CreateRestApiHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton(_assaultCubeExternal);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:30151");
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    if (context.HostingEnvironment.IsDevelopment())
                        logging.AddConsole();
                })
                .Build();
        }

        public async Task MonitorProcesses()
        {
            while (true)
            {
                var processes = Process.GetProcesses();
                var acClient = processes.FirstOrDefault(p => p.ProcessName.Equals("ac_client", StringComparison.OrdinalIgnoreCase));
                var derailValley = processes.FirstOrDefault(p => p.ProcessName.Equals("DerailValley", StringComparison.OrdinalIgnoreCase));
                var gta5 = processes.FirstOrDefault(p => p.ProcessName.Equals("gta5", StringComparison.OrdinalIgnoreCase));

                if (acClient != null && !Vars.assaultCubeInjected)
                {
                    Vars.assaultCubeInjected = true;
                    Console.WriteLine("ac_client is running");
                    Task.Run(() => _assaultCubeExternal.Start());
                }

                if (acClient == null && Vars.assaultCubeInjected)
                {
                    Vars.assaultCubeInjected = false;
                    Task.Run(() => _assaultCubeExternal.End());
                    Console.WriteLine("GameClosed");
                }

                if (derailValley != null && !Vars.derailValleyInjected)
                {
                    Vars.derailValleyInjected = true;
                    Console.WriteLine("derailValley is running");
                }
                
                if (derailValley == null && Vars.derailValleyInjected)
                {
                    Vars.derailValleyInjected = false;
                    Console.WriteLine("GameClosed");
                }
                
                if (gta5 != null)
                {
                    Console.WriteLine("gta5 is running");
                }
                await Task.Delay(3000);
            }
        }
    }
}
