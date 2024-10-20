// SpotifyPlayer.js
import React, { useEffect, useState } from "react";
import axios from "axios";

const CLIENT_ID = "5cf13c1a5734406384e5f6ce468d21a1"; // Replace with your Client ID
const REDIRECT_URI = "http://localhost:3000/callback"; // Your redirect URI
const AUTH_URL = `https://accounts.spotify.com/authorize?client_id=${CLIENT_ID}&response_type=token&redirect_uri=${encodeURIComponent(
  REDIRECT_URI
)}&scope=user-read-private,user-read-email,playlist-read-private`;

const SpotifyPlayer = () => {
  const [accessToken, setAccessToken] = useState(null);
  const [user, setUser] = useState(null);

  // Parse the access token from the URL
  useEffect(() => {
    const hash = window.location.hash;
    let token = window.localStorage.getItem("access_token");

    if (!token && hash) {
      token = hash.split("&")[0].split("=")[1];
      window.location.hash = ""; // Clear the hash
      window.localStorage.setItem("access_token", token);
    }

    setAccessToken(token);
  }, []);

  // Fetch user data from Spotify
  useEffect(() => {
    if (accessToken) {
      axios
        .get("https://api.spotify.com/v1/me", {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then((response) => {
          setUser(response.data);
        })
        .catch((error) => console.error(error));
    }
  }, [accessToken]);

  const handleLogin = () => {
    window.location.href = AUTH_URL;
  };

  const handleLogout = () => {
    setAccessToken(null);
    setUser(null);
    window.localStorage.removeItem("access_token");
  };

  return (
    <div
      style={{
        padding: "20px",
        backgroundColor: "#121212",
        height: "100vh",
        color: "#fff",
      }}
    >
      <button
        style={{
          position: "absolute",
          top: "20px",
          left: "20px",
          backgroundColor: "#1DB954",
          border: "none",
          color: "#fff",
          padding: "10px 20px",
          borderRadius: "20px",
          cursor: "pointer",
        }}
        onClick={user ? handleLogout : handleLogin}
      >
        {user ? user.display_name : "Login"}
      </button>

      <h1>Spotify Music Player</h1>
      {user && <p>Welcome, {user.display_name}!</p>}
      {/* Add your music player components and features here */}
    </div>
  );
};

export default SpotifyPlayer;
