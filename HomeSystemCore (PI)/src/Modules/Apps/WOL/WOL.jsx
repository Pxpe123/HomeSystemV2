import React from "react";
import Iframe from "react-iframe";
import "./WOL.css";

const WOL = () => {
  return (
    <div className="WOL-Container">
      <Iframe url="https://www.netflix.com" className="fill" />
    </div>
  );
};

export default WOL;
