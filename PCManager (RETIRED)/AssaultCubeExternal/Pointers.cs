namespace AssaultCubeHack;

public static class Pointers
{
    public static readonly int GameOffset = 0x17E0A8;

    public static readonly int PlayerOffset = 0x8;
    
    public static readonly int healthOffset = 0xEC;
    
    public static readonly int armourOffset = 0xF0;

    public static readonly int pistolMagAmmoOffset = 0x12C;
    public static readonly int pistolResAmmoOffset = 0x108;
    
    public static readonly int rifleMagAmmoOffset = 0x140;
    public static readonly int rifleResAmmoOffset = 0x11C; //MAYBE

    
    public static readonly int grenadeOffset = 0x144;
    
    public static readonly int[] recoilOffset = { 0x364, 0xC };
    public static readonly int recoil1Offset = 0x60;
    public static readonly int recoil2Offset = 0x5E;
}