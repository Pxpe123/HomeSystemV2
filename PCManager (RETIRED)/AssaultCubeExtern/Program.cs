using System.Threading;
using Swed32;

namespace AssaultCubeExternal
{
    public partial class AssaultCubeMain
    {
        private Swed swed32;
        private IntPtr moduleBase;
        
        public void Start()
        {
            swed32 = new Swed("ac_client");
            moduleBase = swed32.GetModuleBase("ac_client.exe");

            Init();
            PlayerFunctions playerFunctions = new PlayerFunctions(swed32, moduleBase);

            while (true)
            {
                if (Vars.bGodMode)
                {
                    playerFunctions.GodMode();
                }

                if (Vars.bInfAmmo)
                {
                    playerFunctions.InfAmmo();
                }

                if (Vars.bNoRecoil)
                {
                    playerFunctions.NoRecoil();
                }
                Thread.Sleep(5);
            }
        }

        private void Init()
        {
            // Any initialization code here
        }
    }
}