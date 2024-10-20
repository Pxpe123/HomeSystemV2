const WebSocket = require("ws");

const wsClient = new WebSocket("ws://localhost:8080");

wsClient.on("open", () => {
  console.log("Connected to the WebSocket server");

  const message = {
    endpoint: "/audio",
    action: "unmuteVolume",
    deviceName: "SteelSeries Sonar - Media",
    volume: 100,
  };
  wsClient.send(JSON.stringify(message));
});
