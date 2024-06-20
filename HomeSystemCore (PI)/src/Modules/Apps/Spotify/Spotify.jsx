// SubApp.js
import React, { useState, useEffect } from "react";
import SpotifyWebApi from "spotify-web-api-js";
import axios from "axios";
import { Button, Slider, Grid, Typography } from "@mui/material";
import "./Spotify.css";

const spotifyApi = new SpotifyWebApi();

const SubApp = ({ accessToken }) => {
  const [playlists, setPlaylists] = useState([]);
  const [currentTrack, setCurrentTrack] = useState(null);
  const [volume, setVolume] = useState(50);

  useEffect(() => {
    if (accessToken) {
      spotifyApi.setAccessToken(accessToken);
      fetchUserPlaylists();
      fetchCurrentTrack();
    }
  }, [accessToken]);

  const fetchUserPlaylists = async () => {
    try {
      const response = await spotifyApi.getUserPlaylists();
      setPlaylists(response.items);
    } catch (error) {
      console.error("Error fetching playlists", error);
    }
  };

  const fetchCurrentTrack = async () => {
    try {
      const response = await spotifyApi.getMyCurrentPlayingTrack();
      setCurrentTrack(response.item);
    } catch (error) {
      console.error("Error fetching current track", error);
    }
  };

  const handleNextTrack = async () => {
    try {
      await spotifyApi.skipToNext();
      fetchCurrentTrack();
    } catch (error) {
      console.error("Error skipping to next track", error);
    }
  };

  const handlePrevTrack = async () => {
    try {
      await spotifyApi.skipToPrevious();
      fetchCurrentTrack();
    } catch (error) {
      console.error("Error skipping to previous track", error);
    }
  };

  const handleVolumeChange = async (event, newValue) => {
    setVolume(newValue);
    try {
      await spotifyApi.setVolume(newValue);
    } catch (error) {
      console.error("Error setting volume", error);
    }
  };

  return (
    <div className="SubApp-Container">
      <div className="Playlists">
        <Typography variant="h6">Your Playlists</Typography>
        <ul>
          {playlists.map((playlist) => (
            <li key={playlist.id}>{playlist.name}</li>
          ))}
        </ul>
      </div>
      <div className="Player">
        {currentTrack && (
          <>
            <Typography variant="h6">Now Playing</Typography>
            <Typography variant="subtitle1">{currentTrack.name}</Typography>
            <Typography variant="subtitle2">
              {currentTrack.artists.map((artist) => artist.name).join(", ")}
            </Typography>
            <div className="Controls">
              <Button onClick={handlePrevTrack}>Previous</Button>
              <Button onClick={handleNextTrack}>Next</Button>
            </div>
            <div className="VolumeControl">
              <Typography variant="body1">Volume</Typography>
              <Slider
                value={volume}
                onChange={handleVolumeChange}
                aria-labelledby="volume-slider"
              />
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default SubApp;
