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

import WOLApp from "./Modules/Apps/WOL/WOL";
import SpotifyApp from "./Modules/Apps/Spotify/Spotify";
import SteelSeries from "./Modules/Apps/SteelSeries/SteelSeries.jsx";

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
      <div className="App-Holder">
        <Router>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/test" element={<TestFakeComponent />} />
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
