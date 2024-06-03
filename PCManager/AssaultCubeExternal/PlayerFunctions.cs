using System;
using AssaultCubeHack;
using Swed64;

namespace AssaultCubeExternal
{
    public class PlayerFunctions
    {
        private Swed swed32;
        private IntPtr moduleBase;
        private IntPtr LocalPlayerPtr;

        public PlayerFunctions(Swed swed32, IntPtr moduleBase)
        {
            this.swed32 = swed32;
            this.moduleBase = moduleBase;
            LocalPlayerPtr = swed32.ReadPointer(moduleBase + Pointers.GameOffset);
        }

        public void GodMode()
        {
            swed32.WriteInt(LocalPlayerPtr + 0xEC, 999);
        }

        public void InfAmmo()
        {
            swed32.WriteInt(LocalPlayerPtr + Pointers.rifleMagAmmoOffset, 20);
            swed32.WriteInt(LocalPlayerPtr + Pointers.pistolMagAmmoOffset, 10);
        }

        public void NoRecoil()
        {
            IntPtr recoil1Addr = swed32.ReadPointer(moduleBase, 0x364, 0xC, 0x60);
            IntPtr recoil2Addr = swed32.ReadPointer(moduleBase, 0x364, 0xC, 0x5E);

            swed32.WriteInt(recoil1Addr, 0);
            swed32.WriteInt(recoil2Addr, 0);
        }

        public void FillArmourHealth()
        {
            swed32.WriteInt(LocalPlayerPtr + Pointers.healthOffset, 100);
            swed32.WriteInt(LocalPlayerPtr + Pointers.armourOffset, 100);
        }

        public void ResetAmmo()
        {
            swed32.WriteInt(LocalPlayerPtr + Pointers.rifleMagAmmoOffset, 20);
            swed32.WriteInt(LocalPlayerPtr + Pointers.rifleResAmmoOffset, 40);

            swed32.WriteInt(LocalPlayerPtr + Pointers.pistolMagAmmoOffset, 10);
            swed32.WriteInt(LocalPlayerPtr + Pointers.pistolResAmmoOffset, 50);

            swed32.WriteInt(LocalPlayerPtr + Pointers.grenadeOffset, 2);
        }
    }
}