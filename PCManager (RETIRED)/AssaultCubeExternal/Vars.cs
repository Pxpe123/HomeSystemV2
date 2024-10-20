public class Vars
{
    public bool bGodMode { get; set; }
    public bool bInfAmmo { get; set; }
    public bool bNoRecoil { get; set; }

    public void SetGodMode(bool value)
    {
        bGodMode = value;
    }

    public void SetInfAmmo(bool value)
    {
        bInfAmmo = value;
    }

    public void SetNoRecoil(bool value)
    {
        bNoRecoil = value;
    }

    public void ToggleGodMode()
    {
        bGodMode = !bGodMode;
    }

    public void ToggleInfAmmo()
    {
        bInfAmmo = !bInfAmmo;
    }

    public void ToggleNoRecoil()
    {
        bNoRecoil = !bNoRecoil;
    }

    public bool GetGodMode()
    {
        return bGodMode;
    }
    
    public bool GetInfAmmo()
    {
        return bInfAmmo;
    }

    public bool GetNoRecoil()
    {
        return bNoRecoil;
    }
}