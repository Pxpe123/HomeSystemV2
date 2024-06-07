import React, { useState, useEffect } from "react";
import "./AssaultCube.css";
import HomeButton from "../Module/HomeButton/HomeBtn";

import * as Globals from "./../../../../globals";

const AssaultCube = () => {
  const [gameData, setGameData] = useState({
    WeaponData: {
      RifleMagAmmo: 0,
      RifleResAmmo: 0,
      PistolMagAmmo: 0,
      PistolResAmmo: 0,
    },
    PlayerData: {
      Health: 0,
      Armour: 0,
      Grenades: 0,
    },
    CheatData: {
      isGodMode: false,
      isInfAmmo: false,
      isNoRecoil: false,
    },
  });

  const sendDataToPC = async (data) => {
    try {
      const socket = new WebSocket("ws://localhost:30151/Logging");

      return new Promise((resolve, reject) => {
        socket.onopen = () => {
          console.log("WebSocket open:", data);
          socket.send(JSON.stringify(data));
        };

        socket.onmessage = (event) => {
          console.log("WebSocket message:", event.data);
          resolve(JSON.parse(event.data));
          socket.close();
        };

        socket.onerror = (error) => {
          console.error("WebSocket error:", error);
          reject(error);
        };
      });
    } catch (error) {
      console.error("Error:", error);
      return null;
    }
  };

  const ToggleGodMode = async () => {
    console.log("Toggling God Mode");
    const data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "GodMode",
    };

    setGameData((prevState) => ({
      ...prevState,
      CheatData: {
        ...prevState.CheatData,
        isGodMode: !prevState.CheatData.isGodMode,
      },
    }));

    await sendDataToPC(data);
  };

  const ReplenishHealth = async () => {
    const data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "RefillHealth",
    };
    await sendDataToPC(data);
  };

  const ToggleInfAmmo = async () => {
    console.log("Toggling Infinite Ammo");
    const data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "InfAmmo",
    };
    setGameData((prevState) => ({
      ...prevState,
      CheatData: {
        ...prevState.CheatData,
        isInfAmmo: !prevState.CheatData.isInfAmmo,
      },
    }));

    await sendDataToPC(data);
  };

  const RefillAmmo = async () => {
    const data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "RefillAmmo",
    };
    await sendDataToPC(data);
  };

  const NoRecoil = async () => {
    console.log("Toggling No Recoil");
    const data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "NoRecoil",
    };
    setGameData((prevState) => ({
      ...prevState,
      CheatData: {
        ...prevState.CheatData,
        isNoRecoil: !prevState.CheatData.isNoRecoil,
      },
    }));

    await sendDataToPC(data);
  };

  useEffect(() => {
    let cycleCount = 0;

    const syncInterval = setInterval(() => {
      cycleCount += 1;
      console.log(`Cycle count: ${cycleCount}`);

      // Resync with the server every 200 cycles (200 * 100ms = 20 seconds)
      if (cycleCount >= 200) {
        console.log("Resyncing with server");
        GetGameData();
        cycleCount = 0;
      }
    }, 100); // Run every 100ms

    return () => clearInterval(syncInterval);
  }, []);

  const GetGameData = async () => {
    const data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "GetPlayerData",
    };
    const response = await sendDataToPC(data);
    if (response) {
      console.log("Received game data:", response);
      setGameData(response);
    }
  };

  return (
    <div className="AssaultCube-Container">
      <div className="AssaultCube-Data-Container">
        <div className={"AssaultCube-Data-Section-1"}></div>
        <div className={"AssaultCube-Data-Section-2"}></div>
        <div className={"AssaultCube-Data-Section-3"}>
          <div className={"AssaultCube-Right-Items"}>Assault Rifle</div>
          <div className={"AssaultCube-Right-Items"}>Pistol</div>
          <div className={"AssaultCube-Right-Items"}>Grenades</div>
        </div>
      </div>
      <div className="AssaultCube-Cheat-Container">
        <div
          className={`AssaultCube-Cheat-Button ${
            gameData.CheatData.isGodMode ? "Led-Border-On" : "Led-Border-Off"
          }`}
          onClick={ToggleGodMode}
        >
          <div className="Cheat-Button-Top">God Mode</div>
          <div className="Cheat-Button-Bottom">
            <div
              className={`Cheat-Led ${
                gameData.CheatData.isGodMode ? "Led-On" : "Led-Off"
              }`}
            ></div>
          </div>
        </div>
        <div className="AssaultCube-Cheat-Button" onClick={ReplenishHealth}>
          <div className="Cheat-Button-Top2">Refill Health</div>
        </div>
        <div
          className={`AssaultCube-Cheat-Button ${
            gameData.CheatData.isInfAmmo ? "Led-Border-On" : "Led-Border-Off"
          }`}
          onClick={ToggleInfAmmo}
        >
          <div className="Cheat-Button-Top">Infinite Ammo</div>
          <div className="Cheat-Button-Bottom">
            <div
              className={`Cheat-Led ${
                gameData.CheatData.isInfAmmo ? "Led-On" : "Led-Off"
              }`}
            ></div>
          </div>
        </div>
        <div className="AssaultCube-Cheat-Button" onClick={RefillAmmo}>
          <div className="Cheat-Button-Top2">Refill Ammo</div>
        </div>
        <div
          className={`AssaultCube-Cheat-Button ${
            gameData.CheatData.isNoRecoil ? "Led-Border-On" : "Led-Border-Off"
          }`}
          onClick={NoRecoil}
        >
          <div className="Cheat-Button-Top">No Recoil</div>
          <div className="Cheat-Button-Bottom">
            <div
              className={`Cheat-Led ${
                gameData.CheatData.isNoRecoil ? "Led-On" : "Led-Off"
              }`}
            ></div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AssaultCube;
