using System;
using System.Linq;
using NAudio.CoreAudioApi;

namespace AudioDeviceVolumeApp
{
    public class AudioHelper
    {
        private static MMDevice GetDeviceByName(string deviceName)
        {
            var enumerator = new MMDeviceEnumerator();
            var playbackDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            return playbackDevices.FirstOrDefault(device => device.FriendlyName.Contains(deviceName));
        }

        
        //Volume Max { GET SET }
        public float GetVolumeOf(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device != null)
            {
                return device.AudioEndpointVolume.MasterVolumeLevelScalar * 100;
            }
            throw new ArgumentException($"Device '{deviceName}' not found.");
        }
        public void SetVolumeOf(string deviceName, float volume)
        {
            if (volume < 0 || volume > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(volume), "Volume must be between 0 and 100.");
            }

            var device = GetDeviceByName(deviceName);
            if (device != null)
            {
                device.AudioEndpointVolume.MasterVolumeLevelScalar = volume / 100;
            }
            else
            {
                throw new ArgumentException($"Device '{deviceName}' not found.");
            }
        }

        //Current Volume Usage
        public float GetVolumeUsage(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device != null)
            {
                return device.AudioMeterInformation.MasterPeakValue * 100;
            }
            throw new ArgumentException($"Device '{deviceName}' not found.");
        }

        //Channel Muted { GET SET }
        public bool GetMuteStatus(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device != null)
            {
                return device.AudioEndpointVolume.Mute;
            }
            throw new ArgumentException($"Device '{deviceName}' not found.");
        }
        public void SetMuteStatus(string deviceName, bool isMuted)
        {
            var device = GetDeviceByName(deviceName);
            if (device != null)
            {
                device.AudioEndpointVolume.Mute = isMuted;
            }
            throw new ArgumentException($"Device '{deviceName}' not found.");
        }
    }
}
