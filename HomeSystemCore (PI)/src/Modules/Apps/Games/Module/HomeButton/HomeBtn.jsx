import React from "react";
import { Link } from "react-router-dom";
import "./HomeBtn.css";

const HomeBtn = () => {
  return (
    <Link to="/Game" className="home-btn">
      <div className="home-btn-icon">
        <img
          className="home-png-icon"
          src="https://img.icons8.com/ios/50/home-button.png"
          alt="Home"
        />
      </div>
    </Link>
  );
};

export default HomeBtn;
