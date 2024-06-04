import React from "react";
import { BrowserRouter as Router, Link } from "react-router-dom";

const SvgHomeButton = ({ fillColor, strokeColor }) => {
  const handleClick = () => {
    console.log("Button clicked!");
  };

  return (
    <Router>
      <Link to="/" style={{ borderRadius: "50%" }}>
        {" "}
        <svg
          width="80%"
          height="auto"
          viewBox="0 0 80 80"
          xmlns="http://www.w3.org/2000/svg"
        >
          <circle
            cx="40"
            cy="40"
            r="21.6"
            fill={fillColor}
            onClick={handleClick}
          />

          {/* Circle Outline */}
          <circle
            cx="40"
            cy="40"
            r="28"
            fill="none"
            stroke={strokeColor}
            strokeWidth="4"
            onClick={handleClick}
          />

          {/* Invisible Button */}
          <rect
            x="11.2" // Adjusted x position to center the rectangle
            y="11.2" // Adjusted y position to center the rectangle
            width="57.6" // Adjusted width to match the diameter of the outer circle (2 * radius)
            height="57.6" // Adjusted height to match the diameter of the outer circle (2 * radius)
            fill="none"
            id="__btnHome"
            onClick={handleClick}
          />
        </svg>
      </Link>
    </Router>
  );
};

export default SvgHomeButton;
