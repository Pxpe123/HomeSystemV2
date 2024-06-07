using System;
using System.IO;
using System.Management;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;

public class PCInfoMain
{
    public JObject GetPCInfo()
    {
        JObject pcInfo = new JObject();

        // PC Name
        pcInfo["PCName"] = Environment.MachineName;

        // OS Name
        pcInfo["OSName"] = GetOSName();

        // Installed RAM
        pcInfo["InstalledRAM"] = GetInstalledRAM();

        // C: Drive Size
        pcInfo["CDriveSize"] = GetDriveSize("C");
        
        // GPU Info and Usage
        pcInfo["GPU"] = GetGPUInfo();

        // Additional information can be added similarly

        return pcInfo;
    }

    private string GetOSName()
    {
        return (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                select x.GetPropertyValue("Caption")).FirstOrDefault()?.ToString() ?? "Unknown";
    }

    private string GetInstalledRAM()
    {
        double ramAmount = 0;
        foreach (var item in new ManagementObjectSearcher("Select * From Win32_ComputerSystem").Get())
        {
            double.TryParse(item["TotalPhysicalMemory"].ToString(), out ramAmount);
        }
        return (ramAmount / (1024 * 1024 * 1024)).ToString("0.00") + " GB";
    }

    private string GetDriveSize(string driveName)
    {
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady && drive.Name == $"{driveName}:\\")
            {
                return (drive.TotalSize / (1024 * 1024 * 1024)).ToString("0.00") + " GB";
            }
        }
        return "Drive not found";
    }

    private JObject GetCPUInfo()
    {
        JObject cpuInfo = new JObject();
        foreach (var item in new ManagementObjectSearcher("select * from Win32_Processor").Get())
        {
            cpuInfo["Name"] = item["Name"]?.ToString();
            cpuInfo["Speed"] = (Convert.ToDouble(item["CurrentClockSpeed"]) / 1000).ToString("0.00") + " GHz";
            cpuInfo["Usage"] = GetCPUUsage() + " %";
        }
        return cpuInfo;
    }

    private string GetCPUUsage()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        cpuCounter.NextValue();
        System.Threading.Thread.Sleep(1000); // Allow the counter to gather data
        return cpuCounter.NextValue().ToString("0.00");
    }

    private JObject GetRAMUsage()
    {
        var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        var availableRAM = ramCounter.NextValue();
        var totalRAM = Convert.ToDouble(GetInstalledRAM().Replace(" GB", "")) * 1024; // Convert GB to MB
        var usedRAM = totalRAM - availableRAM;
        var usagePercent = (usedRAM / totalRAM) * 100;

        JObject ramUsage = new JObject
        {
            ["TotalRAM"] = totalRAM.ToString("0.00") + " MB",
            ["UsedRAM"] = usedRAM.ToString("0.00") + " MB",
            ["Usage"] = usagePercent.ToString("0.00") + " %"
        };

        return ramUsage;
    }

    private JObject GetGPUInfo()
    {
        JObject gpuInfo = new JObject();
        foreach (var item in new ManagementObjectSearcher("select * from Win32_VideoController").Get())
        {
            gpuInfo["Name"] = item["Name"]?.ToString();
            gpuInfo["RAM"] = (Convert.ToDouble(item["AdapterRAM"]) / (1024 * 1024)).ToString("0.00") + " MB";
            gpuInfo["Usage"] = GetGPUUsage() + " %";
        }
        return gpuInfo;
    }

    private string GetGPUUsage()
    {
        // Implementing GPU usage may require third-party libraries or tools like NVIDIA's NVAPI
        // This is a placeholder as it can be complex and varies by GPU vendor
        return "N/A";
    }
}

