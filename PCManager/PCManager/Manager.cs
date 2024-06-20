using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AssaultCubeExternal;
using F1_Series;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;


namespace PCManager
{
    public class Manager
    {
        private IHost _restApiHost;
        private readonly AssaultCubeMain _assaultCubeExternal = new AssaultCubeMain();
        private readonly F1SeriesMain _f1Series = new F1SeriesMain();
        private readonly PCInfoMain _pcInfo = new PCInfoMain();

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
                var F1Game = processes.FirstOrDefault(p => p.ProcessName.Equals("F1_24", StringComparison.OrdinalIgnoreCase));
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
                
                if (F1Game != null && !Vars.f1SeriesInjected)
                {
                    Vars.f1SeriesInjected = true;
                    _f1Series.StartUp();
                    Console.WriteLine("F1 Series is running");
                }
                
                if (F1Game == null && Vars.f1SeriesInjected)
                {
                    Vars.f1SeriesInjected = false;
                    Console.WriteLine("GameClosed");
                    _f1Series.ShutDown();
                }
                
                if (gta5 != null)
                {
                    Console.WriteLine("gta5 is running");
                }
                
                if (_f1Series != null && _f1Series.gameData != null)
                {
                    
                    var carTelemetryData = _f1Series.gameData["CarTelemetryData"];
                    if (carTelemetryData != null)
                    {

                        // Save carTelemetryData to a JSON file
                        string jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "telem.json");
                        File.WriteAllText(jsonFilePath, _f1Series.gameData.ToString());
                    }

                }
                
                await Task.Delay(3000);
            }
        }
    }
}
