import React, { useEffect, useState } from "react";
import axios from "axios";
import "./DerailValley.css"; // Ensure you have the necessary CSS

const DerailValley = () => {
  const [trainData, setTrainData] = useState({});
  const [locoData, setLocoData] = useState(null);

  const getGameData = async () => {
    const url = "http://localhost:30151/Logging";
    const data = {
      GetData: "DerailValley",
    };

    try {
      const response = await axios.put(url, data);
      return response.data; // Ensure we return the data part of the response
    } catch (error) {
      if (error.response) {
        console.log("Error status:", error.response.status);
        console.log("Error data:", error.response.data);
      } else {
        console.log("Error:", error.message);
      }
    }
  };

  const updateTrainUI = async () => {
    const jsonData = await getGameData();
    if (jsonData) {
      setTrainData(jsonData.TrainData);
      setLocoData(jsonData.LocoData);
    }
  };

  useEffect(() => {
    const intervalId = setInterval(() => {
      updateTrainUI();
    }, 2000); // Update every 2 seconds

    // Cleanup interval on component unmount
    return () => clearInterval(intervalId);
  }, []);

  return (
    <div className="DerailValley-Main">
      <div className="DerailValley-Top">
        <div>
          <iframe></iframe>
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
