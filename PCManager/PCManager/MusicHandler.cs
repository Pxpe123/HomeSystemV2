using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using AudioDeviceVolumeApp;

namespace PCManager
{
    public class MusicHandler
    {
        private readonly List<string> _deviceNames = new List<string>
        {
            "SteelSeries Sonar - Gaming (SteelSeries Sonar Virtual Audio Device)",
            "SteelSeries Sonar - Chat (SteelSeries Sonar Virtual Audio Device)",
            "SteelSeries Sonar - Media (SteelSeries Sonar Virtual Audio Device)",
            "SteelSeries Sonar - Aux (SteelSeries Sonar Virtual Audio Device)",
            "SteelSeries Sonar - Microphone (SteelSeries Sonar Virtual Audio Device)"
        };

        public AudioHelper _AudioManager = new AudioHelper();

        public JObject GetAudioData()
        {
            JObject audioData = new JObject();
            foreach (string deviceName in _deviceNames)
            {
                JObject deviceInfo = new JObject();
                deviceInfo["Volume"] = _AudioManager.GetVolumeOf(deviceName);
                deviceInfo["CurrentUsage"] = _AudioManager.GetVolumeUsage(deviceName);
                deviceInfo["isMuted"] = _AudioManager.GetMuteStatus(deviceName);

                audioData[deviceName] = deviceInfo;
            }

            return audioData;
        }

        public void HandleMusicData(JObject Data)
        {
            string Method = "";
            if (Data.ContainsKey("Method"))
            {
                Method = Data["Method"].ToString();
            }

            if (Method == "SetVolume")
            {
                string Volume = Data["Volume"].ToString();
                string DeviceName = Data["DeviceName"].ToString();

                _AudioManager.SetVolumeOf(DeviceName, float.Parse(Volume));
            }

            if (Method == "SetMuted")
            {
                string Volume = Data["Muted"].ToString();
                string DeviceName = Data["DeviceName"].ToString();

                _AudioManager.SetMuteStatus(DeviceName, bool.Parse(Volume));
            }

            if (Method == "GetVolume")
            {
                string DeviceName = Data["DeviceName"].ToString();
                float Volume = _AudioManager.GetVolumeOf(DeviceName);
            }

            if (Method == "GetMuted")
            {
                string DeviceName = Data["DeviceName"].ToString();
                bool isMuted = _AudioManager.GetMuteStatus(DeviceName);
            }

            if (Method == "GetAudioData")
            {
                JObject audioData = new JObject();
                foreach (string deviceName in _deviceNames)
                {
                    JObject deviceInfo = new JObject();
                    deviceInfo["Volume"] = _AudioManager.GetVolumeOf(deviceName);
                    deviceInfo["CurrentUsage"] = _AudioManager.GetVolumeUsage(deviceName);
                    deviceInfo["isMuted"] = _AudioManager.GetMuteStatus(deviceName);

                    audioData[deviceName] = deviceInfo;
                }
                
            }
        }
    }
}
