import React, { useEffect } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "./Layout.css";

import * as Globals from "./globals.js";

import HomeBackground from "./resource/Home/background.jpg";

import "./vars.css";

//Layout
import Footer from "./Modules/Footer/Footer";

//Home
import Home from "./Modules/Home/Home";

//Misc Apps

//Game App
import GameApp from "./Modules/Apps/Games/Home/Home";
import WOLApp from "./Modules/Apps/WOL/WOL";
import SpotifyApp from "./Modules/Apps/Spotify/Spotify";

//Games
import DerailValley from "./Modules/Apps/Games/DerailValley/DerailValley";
import AssaultCube from "./Modules/Apps/Games/AssaultCube/AssaultCube";
import SteelSeries from "./Modules/Apps/SteelSeries/SteelSeries";
import Notification from "./Modules/Global Components/Notifications/Notification";

//_________________________//
//
//
//
//
//
//
//
//
//
//
//
//
//
//_________________________//

const TestFakeComponent = () => {
  return (
    <div style={{ width: "100%", height: "100%", backgroundColor: "gray" }}>
      <h1 style={{ marginTop: "0px" }}>Test APP</h1>
    </div>
  );
};

const Layout = () => {
  useEffect(() => {
    document.body.classList.add("dark-mode", "accent-red");

    return () => {
      document.body.classList.remove("dark-mode", "accent-white");
    };
  }, []);
  return (
    <div style={{ height: "100vh", width: "100vw" }}>
      <Notification />
      <div className="App-Holder">
        <Router>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/test" element={<TestFakeComponent />} />
            <Route path="/Game" element={<GameApp />} />
            <Route path="/Spotify" element={<SpotifyApp />} />
            <Route path="/WOL" element={<WOLApp />} />
            <Route path="/SteelSeries" element={<SteelSeries />} />
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"----------------------------------------"}
            {"--------------Game Section--------------"}
            <Route path="/Game/AssaultCube" element={<AssaultCube />} />
            <Route path="/Game/DerailValley" element={<DerailValley />} />
          </Routes>
        </Router>
      </div>
      <div className="Footer-Holder">
        <Footer />
      </div>
    </div>
  );
};

export default Layout;
