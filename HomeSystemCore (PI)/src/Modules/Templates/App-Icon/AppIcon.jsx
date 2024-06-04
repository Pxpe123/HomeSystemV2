import React from "react";
import { Link } from "react-router-dom";

const AppIcon = ({ src, givenPath, appName }) => {
  return (
    <div
      style={{
        width: "100%",
        aspectRatio: 1 / 1,
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <Link to={givenPath} style={{ textDecoration: "none" }}>
        <div
          style={{
            width: "100px",
            height: "100px",
            borderRadius: "50%",
            backgroundColor: "blue",
            cursor: "pointer",
          }}
        >
          <img
            src={src}
            alt="circle-img"
            style={{
              width: "80px",
              height: "80px",
              borderRadius: "50%",
              objectFit: "cover",
            }}
          />
        </div>
      </Link>
      <p style={{ marginTop: "10px", color: "white" }}>{appName}</p>{" "}
    </div>
  );
};

export default AppIcon;
