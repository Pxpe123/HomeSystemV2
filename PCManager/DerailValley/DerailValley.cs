using System.Threading;
using Swed64;

namespace DerailValley
{
    public partial class AssaultCubeMain
    {
        public PlayerFunctions PlayerFunctions { get; private set; }
        private Swed swed32;
        public Vars Vars = new Vars(); // Initialize the Vars field
        private IntPtr moduleBase;
        private bool isRunning; // Flag to control the while loop


        public AssaultCubeMain()
        {
            swed32 = new Swed("ac_client");
            moduleBase = swed32.GetModuleBase("ac_client.exe");
        }
        
        public void Start()
        {
            isRunning = true;
            
            while (isRunning)
            {

                Thread.Sleep(5);
            }
        }

        public void End()
        {
            isRunning = false;
        }
        
        
        public static IntPtr DereferencePointer(IntPtr baseAddress, int[] offsets, Swed swed)
        {
            IntPtr currentPointer = baseAddress;
            Console.WriteLine("Base Address: " + currentPointer);

            for (int i = 0; i < offsets.Length; i++)
            {
                currentPointer = (IntPtr)(swed.ReadInt((nint)(long)currentPointer) + offsets[i]);
                Console.WriteLine($"After offset {offsets[i]}, pointer is: " + currentPointer);
            }

            return currentPointer;
        }
    }
}