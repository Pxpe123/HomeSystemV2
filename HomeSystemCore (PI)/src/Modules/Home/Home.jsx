import React from "react";
import "./Home.css";
import AppIcon from "./../Templates/App-Icon/AppIcon";
import HomeBackground from "../../resource/Home/background.jpg";
import GamesIcon from "./Icons/Games"; // Import your GamesIcon component

import SpotifyIcon from "./Icons/pngegg.png";

const apps = [
  {
    appName: "Spotify",
    givenPath: "/test",
    src: SpotifyIcon,
    isFunctional: false,
  },
  {
    appName: "Gaming Hub",
    givenPath: "/Game",
    src: "https://img.icons8.com/?size=100&id=P74xrK8LVXNo&format=png&color=ffffff",
    isFunctional: true,
  },
  {
    appName: "Test App 3",
    givenPath: "/Youtube",
    src: "https://img.icons8.com/ios-filled/100/power-off-button.png",
    isFunctional: false,
  },
  {
    appName: "Test App 4",
    givenPath: "/test4",
    src: "https://img.icons8.com/ios-filled/100/power-off-button.png",
    isFunctional: false,
  },
  {
    appName: "Test App 5",
    givenPath: "/test5",
    src: "https://img.icons8.com/ios-filled/100/power-off-button.png",
    isFunctional: false,
  },
  // More apps...
];

const Home = () => {
  const containerStyle = {
    backgroundImage: `url(${HomeBackground})`,
    backgroundSize: "cover",
    backgroundPosition: "center",
  };

  return (
    <div className="Home-Container" style={containerStyle}>
      {apps.map((app, index) => (
        <AppIcon
          key={index}
          appName={app.appName}
          givenPath={app.givenPath}
          src={app.src}
          isFunctional={app.isFunctional}
        />
      ))}
    </div>
  );
};

export default Home;
