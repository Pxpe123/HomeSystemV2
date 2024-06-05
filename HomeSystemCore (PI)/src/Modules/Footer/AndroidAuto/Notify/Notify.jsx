import React from "react";
import "./Notify.css";
import NotifyBell from "./resource/NotifyBell";
import NotifyBellRung from "./resource/NotifyBellRung";

const Notify = ({ bellColour, bellColourRung, isRung }) => {
  return (
    <div className="notifyButton-Container">
      {isRung ? (
        <NotifyBellRung strokeColor={bellColourRung} />
      ) : (
        <NotifyBell strokeColor={bellColour} />
      )}
    </div>
  );
};

export default Notify;
