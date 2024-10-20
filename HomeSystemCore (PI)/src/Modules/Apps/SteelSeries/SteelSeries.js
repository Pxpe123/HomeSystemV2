const ws = new WebSocket("ws://localhost:8080");

ws.onopen = () => {
  ws.send(
    JSON.stringify({
      endpoint: "/audio",
      action: "setVolume",
      deviceName: "SteelSeries Sonar - Gaming",
      volume: 50,
    })
  );
};
