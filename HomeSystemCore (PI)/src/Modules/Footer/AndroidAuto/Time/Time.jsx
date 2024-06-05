import React, { useState, useEffect } from "react";
import dayjs from "dayjs";
import "./Time.css";

const TimeLayout = () => {
  const [currentTime, setCurrentTime] = useState(dayjs());

  useEffect(() => {
    const timer = setInterval(() => {
      setCurrentTime(dayjs());
    }, 1000);

    return () => clearInterval(timer);
  }, []);

  return (
    <div className="Time-Container">
      <div className="Time-Date">{currentTime.format("ddd (MM/DD)")}</div>
      <div className="Time-Time">{currentTime.format("hh:mm A")}</div>
    </div>
  );
};

export default TimeLayout;
