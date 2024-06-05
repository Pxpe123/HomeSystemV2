import React from "react";

const SvgHomeButton = ({ fillColor, strokeColor }) => {
  return (
    <a href="/">
      <svg
        width="80%"
        height="auto"
        viewBox="0 0 80 80"
        xmlns="http://www.w3.org/2000/svg"
      >
        <circle cx="40" cy="40" r="21.6" fill={fillColor} />

        {/* Circle Outline */}
        <circle
          cx="40"
          cy="40"
          r="28"
          fill="none"
          stroke={strokeColor}
          strokeWidth="4"
        />

        {/* Invisible Button */}
        <rect
          x="11.2"
          y="11.2"
          width="57.6"
          height="57.6"
          fill="none"
          id="__btnHome"
        />
      </svg>
    </a>
  );
};

export default SvgHomeButton;
