using System.Threading.Tasks;

namespace PCManager
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Manager manager = new Manager();
            var initTask = manager.Init(args);
            var monitorTask = manager.MonitorProcesses();
            await Task.WhenAll(initTask, monitorTask);
        }
    }
}
 