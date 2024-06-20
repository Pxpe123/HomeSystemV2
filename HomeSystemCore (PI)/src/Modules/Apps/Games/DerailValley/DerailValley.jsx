import React, { useEffect, useState, useRef } from "react";
import "./DerailValley.css"; // Ensure you have the necessary CSS

const DerailValley = () => {
  const [trainData, setTrainData] = useState({});
  const [locoData, setLocoData] = useState(null);
  const socketRef = useRef(null);

  const iframeSrc = `http://:@localhost:7245`;

  useEffect(() => {
    const url = "ws://localhost:30151/Logging";
    const data = {
      Type: "Game",
      Game: "DerailValley",
    };

    const socket = new WebSocket(url);
    socketRef.current = socket;

    // Event listener for when the WebSocket connection is opened
    socket.onopen = () => {
      console.log("WebSocket connection opened");
      // Send data instantly after the connection is opened
      socket.send(JSON.stringify(data));
    };

    // Event listener for receiving messages from the server
    socket.onmessage = (event) => {
      try {
        // Parse the incoming message
        const response = JSON.parse(event.data);
        console.log(response);
        setTrainData(response.TrainData);
        setLocoData(response.LocoData);
      } catch (error) {
        console.error(`Error parsing response: ${error.message}`);
      }
    };

    // Event listener for handling errors
    socket.onerror = (error) => {
      console.error(`WebSocket error: ${error.message}`);
    };

    // Event listener for when the WebSocket connection is closed
    socket.onclose = (event) => {
      console.error(
        `WebSocket closed with code: ${event.code}, reason: ${event.reason}`
      );
    };

    // Cleanup WebSocket connection on component unmount
    return () => {
      if (socketRef.current) {
        socketRef.current.close();
      }
    };
  }, []);

  useEffect(() => {
    const sendData = () => {
      if (
        socketRef.current &&
        socketRef.current.readyState === WebSocket.OPEN
      ) {
        const data = {
          Type: "Game",
          Game: "DerailValley",
        };
        socketRef.current.send(JSON.stringify(data));
      }
    };

    // Send data instantly
    sendData();

    // Send data every 2 seconds
    const intervalId = setInterval(sendData, 2000);

    // Cleanup interval on component unmount
    return () => clearInterval(intervalId);
  }, []);

  return (
    <div className="DerailValley-Main">
      <div className="DerailValley-Top">
        <div>
        
        </div>
        <div></div>
      </div>
      <div className="DerailValley-Bottom">
        {trainData &&
          Object.keys(trainData).map((trainId) => (
            <div key={trainId} className="train-div">
              {trainId}
            </div>
          ))}
      </div>
    </div>
  );
};

export default DerailValley;
