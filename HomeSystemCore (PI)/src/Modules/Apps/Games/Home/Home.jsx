import React from "react";
import "./Home.css";
import AppIcon from "../../../Templates/App-Icon/AppIcon";

// Game Icons //
import AssaultCubeIcon from "./resource/AssaultCube.png";
import DerailValleyIcon from "./resource/DerailValley.png";

const Home = () => {
  return (
    <div className="GameApp-Container">
      <div className="GameApp-Header">
        <h1 className="GameApp-Title">Game Hub</h1>
      </div>
      <div className="GameApp-Body">
        <AppIcon
          givenPath={"./AssaultCube"}
          appName={"Assault Cube"}
          isFunctional={true}
          src={AssaultCubeIcon}
        />
        <AppIcon
          givenPath={"./DerailValley"}
          appName={"Derail Valley"}
          isFunctional={true}
          src={DerailValleyIcon}
        />
      </div>
    </div>
  );
};

export default Home;
