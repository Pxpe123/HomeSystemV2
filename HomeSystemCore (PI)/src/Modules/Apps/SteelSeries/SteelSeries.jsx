import React, { useEffect, useState, useRef } from "react";
import "./SteelSeries.css";
import debounce from "lodash/debounce";

const SteelSeries = () => {
  const devices = [
    { name: "Master", key: "Master" },
    {
      name: "Game",
      key: "SteelSeries Sonar - Gaming (SteelSeries Sonar Virtual Audio Device)",
    },
    {
      name: "Chat",
      key: "SteelSeries Sonar - Chat (SteelSeries Sonar Virtual Audio Device)",
    },
    {
      name: "Media",
      key: "SteelSeries Sonar - Media (SteelSeries Sonar Virtual Audio Device)",
    },
    {
      name: "Aux",
      key: "SteelSeries Sonar - Aux (SteelSeries Sonar Virtual Audio Device)",
    },
    {
      name: "Microphone",
      key: "SteelSeries Sonar - Microphone (SteelSeries Sonar Virtual Audio Device)",
    },
  ];

  const [volumes, setVolumes] = useState(
    devices.reduce((acc, device) => {
      acc[device.key] = 0;
      return acc;
    }, {})
  );

  const [muted, setMuted] = useState(
    devices.reduce((acc, device) => {
      acc[device.key] = false;
      return acc;
    }, {})
  );

  const socketRef = useRef(null);

  useEffect(() => {
    const socket = new WebSocket("ws://localhost:30151");
    socketRef.current = socket;

    socket.onopen = () => {
      console.log("WebSocket connection established.");
      // Fetch audio data frequently
      const intervalId = setInterval(() => {
        const message = JSON.stringify({ Type: "GetAudioData" });
        socket.send(message);
      }, 250);

      return () => clearInterval(intervalId);
    };

    socket.onmessage = (event) => {
      const data = JSON.parse(event.data);
      const newVolumes = { ...volumes };
      const newMuted = { ...muted };

      devices.forEach((device) => {
        if (data[device.key]) {
          newVolumes[device.key] = data[device.key].Volume;
          newMuted[device.key] = data[device.key].isMuted;
        }
      });

      setVolumes(newVolumes);
      setMuted(newMuted);
    };

    socket.onerror = (error) => console.error("WebSocket error:", error);
    socket.onclose = () => console.log("WebSocket connection closed.");

    return () => {
      socket.close();
    };
  }, []);

  const handleVolumeChange = debounce((deviceKey, value) => {
    setVolumes((prevVolumes) => ({
      ...prevVolumes,
      [deviceKey]: value,
    }));

    const message = JSON.stringify({
      Type: "MusicData",
      SetVolume: value,
      DeviceName: deviceKey,
    });
    if (socketRef.current.readyState === WebSocket.OPEN) {
      socketRef.current.send(message);
    }
  }, 100);

  const handleMuteToggle = (deviceKey) => {
    setMuted((prevMuted) => {
      const newMutedValue = !prevMuted[deviceKey];

      const message = JSON.stringify({
        Type: "MusicData",
        SetMuted: deviceKey,
        Muted: newMutedValue,
      });
      if (socketRef.current.readyState === WebSocket.OPEN) {
        socketRef.current.send(message);
      }

      return {
        ...prevMuted,
        [deviceKey]: newMutedValue,
      };
    });
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
                style={{ transform: "rotate(-90deg)" }}
                onChange={(e) =>
                  handleVolumeChange(device.key, parseInt(e.target.value, 10))
                }
              />
            </div>
            <div className="Slider-Item-Bottom">
              <button
                id={`${device.key}_Mute_Btn`}
                onClick={() => handleMuteToggle(device.key)}
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
