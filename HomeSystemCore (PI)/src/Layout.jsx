import React, { useEffect } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "./Layout.css";

import HomeBackground from "./resource/Home/background.jpg";

import "./vars.css";

//Layout
import Footer from "./Modules/Footer/Footer";

//Home
import Home from "./Modules/Home/Home";

//Misc Apps

//Games
import DerailValley from "./Modules/Apps/Games/DerailValley/DerailValley";

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

// Fake Component for /test route
const TestFakeComponent = () => {
  return <h1>This is a fake component for the /test route</h1>;
};

const Layout = () => {
  useEffect(() => {
    document.body.classList.add("dark-mode", "accent-white");

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
            <Route path="/test" element={<TestFakeComponent />} />{" "}
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
