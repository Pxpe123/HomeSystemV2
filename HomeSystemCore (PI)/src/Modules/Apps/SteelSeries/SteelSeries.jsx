import React, { useState } from "react";
import "./SteelSeries.css";
import "./SteelSeries.js";

const SteelSeries = () => {
  const devices = [
    { name: "Master", key: "Master" },
    { name: "Game", key: "Game Audio" },
    { name: "Chat", key: "Chat Audio" },
    { name: "Media", key: "Media Audio" },
    { name: "Aux", key: "Aux Audio" },
    { name: "Microphone", key: "Microphone Audio" },
  ];

  const [volumes, setVolumes] = useState(
    devices.reduce((acc, device) => {
      acc[device.key] = 50; // Default volume
      return acc;
    }, {})
  );

  const [muted, setMuted] = useState(
    devices.reduce((acc, device) => {
      acc[device.key] = false; // Default unmuted
      return acc;
    }, {})
  );

  const handleVolumeChange = (deviceKey, value) => {
    setVolumes((prevVolumes) => ({
      ...prevVolumes,
      [deviceKey]: value,
    }));
  };

  const handleMuteToggle = (deviceKey) => {
    setMuted((prevMuted) => ({
      ...prevMuted,
      [deviceKey]: !prevMuted[deviceKey],
    }));
  };

  return (
    <div className="SteelSeries-Container">
      <div className="Slider-Container">
        {devices.map((device) => (
          <div className="Slider-Item" key={device.key}>
            <div className={`Slider-Item-Top ${device.name}`}>
              {device.name}
            </div>
            <div className="Slider-Item-Middle">
              <input
                type="range"
                min="0"
                max="100"
                value={volumes[device.key]}
                className="slider-input"
                onChange={(e) =>
                  handleVolumeChange(device.key, parseInt(e.target.value, 10))
                }
              />
            </div>
            <div className="Slider-Item-Bottom">
              <button
                id={`${device.key}_Mute_Btn`}
                onClick={() => handleMuteToggle(device.key)}
                style={{
                  backgroundColor: muted[device.key] ? "#d9534f" : "#05cf9c", // Red if muted, green if not
                  color: "#fff", // Keep text color white for contrast
                  borderRadius: "5px", // Ensure the button has rounded corners
                  width: "80%", // Maintain consistent width
                  height: "70%", // Maintain consistent height
                  cursor: "pointer", // Pointer cursor
                  transition: "background-color 0.3s ease", // Smooth transition for background color
                }}
              >
                {muted[device.key] ? "Unmute" : "Mute"}
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default SteelSeries;
