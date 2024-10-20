process.env.EDGE_USE_CORECLR = 1;

// index.js
const WebSocket = require("ws");
const { handleAudio } = require("./handlers/audio");

//const ExampleClient = require("./ExampleClient");

const wss = new WebSocket.Server({ port: 8080 }, () => {
  console.log("WebSocket server started on ws://localhost:8080");
});

const handleMessage = async (message, ws) => {
  try {
    const data = JSON.parse(message);

    switch (data.endpoint) {
      case "/audio":
        await handleAudio(data, ws);
        break;
      default:
        ws.send(JSON.stringify({ error: "Unknown endpoint" }));
    }
  } catch (error) {
    console.error("Error handling message:", error);
    ws.send(JSON.stringify({ error: "Invalid message format" }));
  }
};

wss.on("connection", (ws) => {
  console.log("Client connected");

  ws.on("message", (message) => {
    const messageStr = message.toString();
    console.log("Received message:", messageStr);
    handleMessage(messageStr, ws);
  });

  ws.on("close", () => {
    console.log("Client disconnected");
  });
});
