import React from "react";
import "./Footer.css";

import SvgHomeButton from "./AndroidAuto/HomeButton/HomeButton";

import ActivityBar from "./AndroidAuto/ActivityBar/Activities";

import TimeLayout from "./AndroidAuto/Time/Time";

import Notify from "./AndroidAuto/Notify/Notify";

const Footer = () => {
  var Activities = false;
  var isRung = false;

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
      <div className="AndroidButton-Notify-Div">
        <Notify bellColour="#fff" bellColourRung="#fff" isRung={isRung} />
      </div>
      <div className="Time-Div">
        <TimeLayout />
      </div>
    </div>
  );
};

export default Footer;
