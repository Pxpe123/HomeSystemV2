import React from "react";
import { Link } from "react-router-dom";

const AppIcon = ({ src, givenPath, appName, isFunctional }) => {
  return (
    <div
      style={{
        width: "60%",
        marginLeft: "20%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <div
        style={{
          width: "100%",
          paddingTop: "100%", // This maintains the aspect ratio
          position: "relative",
          overflow: "hidden",
          borderRadius: "50%", // Rounded border for the container
        }}
      >
        <Link
          to={givenPath}
          style={{
            textDecoration: "none",
            width: "100%",
            height: "100%",
            position: "absolute",
            top: 0,
            left: 0,
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            borderRadius: "50%", // Rounded border for the Link
          }}
        >
          {typeof src === "string" ? (
            <img
              src={src}
              alt="circle-img"
              style={{
                width: "100%",
                height: "100%",
                objectFit: "cover",
                borderRadius: "50%", // Rounded border for the img
              }}
            />
          ) : (
            <div
              style={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                width: "100%",
                height: "100%",
              }}
            >
              {src}
            </div>
          )}
        </Link>
      </div>
      <p
        style={{
          marginTop: "10px",
          color: "white",
          textWrap: "nowrap",
          fontWeight: "bold",
          ...(!isFunctional ? { textDecoration: "line-through" } : {}),
        }}
      >
        {appName}
      </p>
    </div>
  );
};

export default AppIcon;
