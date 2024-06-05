import React from "react";
import "./AssaultCube.css";

const AssaultCube = () => {
  function sendDataToPC(data) {
    fetch("http://localhost:30151/Logging", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.text().then((text) => {
          return text ? JSON.parse(text) : {};
        });
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }

  function ToggleGodMode() {
    var data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "GodMode",
    };
    sendDataToPC(data);
  }

  function ReplenishHealth() {
    var data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "RefillHealth",
    };
    sendDataToPC(data);
  }

  function ToggleInfAmmo() {
    var data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "InfAmmo",
    };
    sendDataToPC(data);
  }

  function RefillAmmo() {
    var data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "RefillAmmo",
    };
    sendDataToPC(data);
  }

  function NoRecoil() {
    var data = {
      Type: "Game",
      Game: "AssaultCube",
      Value: "NoRecoil",
    };
    sendDataToPC(data);
  }

  return (
    <div className="AssaultCube-Container">
      <div className="AssaultCube-Data-Container">
        <div></div>
      </div>
      <div className="AssaultCube-Cheat-Container">
        <div className="AssaultCube-Cheat-Button" onClick={ToggleGodMode}>
          GodMode
        </div>
        <div className="AssaultCube-Cheat-Button" onClick={ReplenishHealth}>
          Replenish Health
        </div>
        <div className="AssaultCube-Cheat-Button" onClick={ToggleInfAmmo}>
          Toggle Inf Ammo
        </div>
        <div className="AssaultCube-Cheat-Button" onClick={RefillAmmo}>
          Refill Ammo
        </div>
        <div className="AssaultCube-Cheat-Button" onClick={NoRecoil}>
          No Recoil
        </div>
      </div>
    </div>
  );
};

export default AssaultCube;
