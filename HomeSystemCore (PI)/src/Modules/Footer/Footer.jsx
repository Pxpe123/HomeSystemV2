import React from "react";
import "./Footer.css";

import SvgHomeButton from "./AndroidAuto/HomeButton/HomeButton";

import ActivityBar from "./AndroidAuto/ActivityBar/Activities";

const Footer = () => {
  var Activities = true;

  return (
    <div className="Footer-Container">
      <div className="AndroidButton-Div">
        <SvgHomeButton
          className="AndroidSVGButton"
          fillColor="white"
          strokeColor="white"
        />
      </div>
      <div className="AndroidButton-Activity-Div">
        {Activities && <ActivityBar />}
      </div>
      <div className="AndroidButton-Notify-Div"></div>
      <div className="Time-Div"></div>
    </div>
  );
};

export default Footer;
