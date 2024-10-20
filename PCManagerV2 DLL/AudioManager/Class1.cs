using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi;
using System.Threading.Tasks;

namespace AudioManager
{
    public class VolumeManager
    {
        private readonly MMDeviceEnumerator deviceEnumerator;

        public VolumeManager()
        {
            deviceEnumerator = new MMDeviceEnumerator();
        }

        private MMDevice GetDeviceByName(string deviceName)
        {
            foreach (var device in deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                if (device.FriendlyName.Equals(deviceName, StringComparison.OrdinalIgnoreCase))
                {
                    return device;
                }
            }
            return null;
        }

        public float GetVolume(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device == null)
                throw new ArgumentException($"Device '{deviceName}' not found.");

            return device.AudioEndpointVolume.MasterVolumeLevelScalar * 100; // Convert to percentage
        }

        public void SetVolume(string deviceName, float volume)
        {
            if (volume < 0 || volume > 100)
                throw new ArgumentOutOfRangeException(nameof(volume), "Volume must be between 0 and 100.");

            var device = GetDeviceByName(deviceName);
            if (device == null)
                throw new ArgumentException($"Device '{deviceName}' not found.");

            device.AudioEndpointVolume.MasterVolumeLevelScalar = volume / 100; // Set volume between 0.0 and 1.0
        }

        public bool IsMuted(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device == null)
                throw new ArgumentException($"Device '{deviceName}' not found.");

            return device.AudioEndpointVolume.Mute;
        }

        public void Mute(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device == null)
                throw new ArgumentException($"Device '{deviceName}' not found.");

            device.AudioEndpointVolume.Mute = true;
        }

        public void Unmute(string deviceName)
        {
            var device = GetDeviceByName(deviceName);
            if (device == null)
                throw new ArgumentException($"Device '{deviceName}' not found.");

            device.AudioEndpointVolume.Mute = false;
        }
    }
}
