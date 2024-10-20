const { exec } = require("child_process");
const path = require("path");

// Set the path to nircmd.exe
const nircmdPath = path.join(__dirname, "Dependencies", "NirCmd", "nircmd.exe");

const muteVolume = (deviceName, ws) => {
  const command = `"${nircmdPath}" mutesysvolume 1 "${deviceName}"`;

  exec(command, (err) => {
    if (err) {
      ws.send(
        JSON.stringify({
          error: `Could not mute volume for ${deviceName}: ${err.message}`,
        })
      );
    } else {
      console.log(`Muting volume for ${deviceName}`);
      ws.send(JSON.stringify({ success: true }));
    }
  });
};

const unmuteVolume = (deviceName, ws) => {
  const command = `"${nircmdPath}" mutesysvolume 0 "${deviceName}"`;

  exec(command, (err) => {
    if (err) {
      ws.send(
        JSON.stringify({
          error: `Could not mute volume for ${deviceName}: ${err.message}`,
        })
      );
    } else {
      console.log(`Muting volume for ${deviceName}`);
      ws.send(JSON.stringify({ success: true }));
    }
  });
};

// Function to set the volume using NirCmd
const setVolume = (deviceName, percentage, ws) => {
  const volume = Math.round((percentage / 100) * 65535);
  const command = `"${nircmdPath}" setsysvolume ${volume} "${deviceName}"`;

  exec(command, (err) => {
    if (err) {
      ws.send(
        JSON.stringify({
          error: `Could not set volume for ${deviceName}: ${err.message}`,
        })
      );
    } else {
      console.log(
        `Setting volume of ${deviceName} to: ${percentage}% (${volume})`
      );
      ws.send(JSON.stringify({ success: true }));
    }
  });
};

const handleAudio = (data, ws) => {
  const deviceName = data.deviceName;
  const action = data.action;
  const volume = data.volume;

  switch (action) {
    case "getVolume":
      getVolume(deviceName, ws);
      break;

    case "setVolume":
      setVolume(deviceName, volume, ws);
      console.log(deviceName);
      break;

    case "unmuteVolume":
      unmuteVolume(deviceName, ws);
      break;

    case "muteVolume":
      muteVolume(deviceName, ws);
      break;

    default:
      ws.send(JSON.stringify({ error: "Unknown action for /audio" }));
  }
};

module.exports = { handleAudio };
