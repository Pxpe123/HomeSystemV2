import React from "react";
import "./Home.css";

import AppIcon from "./../Templates/App-Icon/AppIcon";

// Assuming HomeBackground image is located in the resource/Home directory
import HomeBackground from "../../resource/Home/background.jpg";

const Home = () => {
  const containerStyle = {
    backgroundImage: `url(${HomeBackground})`,
    backgroundSize: "cover",
    backgroundPosition: "center",
  };
  return (
    <div className="Home-Container" style={containerStyle}>
      {/* <AppIcon appName={"Test App"} givenPath={"/test"} /> */}
    </div>
  );
};

export default Home;
