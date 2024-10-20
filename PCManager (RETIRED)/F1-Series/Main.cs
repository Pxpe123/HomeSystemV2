using F1Sharp;
using F1Sharp.Data;
using F1Sharp.Packets;
using Newtonsoft.Json.Linq;
using System;

namespace F1_Series
{
    public class F1SeriesMain
    {
        private TelemetryClient GameClient;
        public JObject gameData; // Define gameData at the class level

        public void StartUp()
        {
            GameClient = new TelemetryClient(20777);
            GameClient.OnCarTelemetryDataReceive += Client_OnCarTelemetryDataReceive;
            //GameClient.OnSessionDataReceive += Client_OnSessionDataRecieve;
            GameClient.OnParticipantsDataReceive += ClientOnParticipantsDataReceive;
        }

        public void ShutDown()
        {
            if (GameClient != null)
            {
                GameClient.Stop();
                GameClient.OnCarTelemetryDataReceive -= Client_OnCarTelemetryDataReceive;
                //GameClient.OnSessionDataReceive -= Client_OnSessionDataRecieve;
                GameClient.OnParticipantsDataReceive -= ClientOnParticipantsDataReceive;
                GameClient = null;
            }
        }
        
        private void ClientOnParticipantsDataReceive(ParticipantsPacket packet)
        {
            Console.WriteLine("ParciPacket");
            gameData["ParticipantsData"] = new JObject
            {
                ["header"] = JObject.FromObject(packet.header),
                ["numActiveCars"] = packet.numActiveCars,
                ["participants"] = new JArray(packet.participants.Select(p => JObject.FromObject(p)))
            };
            Console.WriteLine(gameData["ParticipantsData"].ToString());
        }

        private void Client_OnSessionDataRecieve(SessionPacket packet)
        {
            JObject sessionData = new JObject
            {
                ["weather"] = packet.weather.ToString(),
                ["trackTemperature"] = packet.trackTemperature,
                ["airTemperature"] = packet.airTemperature,
                ["totalLaps"] = packet.totalLaps,
                ["trackLength"] = packet.trackLength,
                ["sessionType"] = packet.sessionType.ToString(),
                ["trackId"] = packet.trackId.ToString(),
                ["formula"] = packet.formula.ToString(),
                ["sessionTimeLeft"] = packet.sessionTimeLeft,
                ["sessionDuration"] = packet.sessionDuration,
                ["pitSpeedLimit"] = packet.pitSpeedLimit,
                ["gamePaused"] = packet.gamePaused,
                ["isSpectating"] = packet.isSpectating,
                ["spectatorCarIndex"] = packet.spectatorCarIndex,
                ["sliProNativeSupport"] = packet.sliProNativeSupport,
                ["numMarshalZones"] = packet.numMarshalZones,
                ["marshalZones"] = new JArray(packet.marshalZones.Select(mz => JObject.FromObject(mz))),
                ["safetyCarStatus"] = packet.safetyCarStatus.ToString(),
                ["networkGame"] = packet.networkGame,
                ["numWeatherForecastSamples"] = packet.numWeatherForecastSamples,
                ["weatherForecastSamples"] = new JArray(packet.weatherForecastSamples.Select(wfs => JObject.FromObject(wfs))),
                ["forecastAccuracy"] = packet.forecastAccuracy,
                ["aiDifficulty"] = packet.aiDifficulty,
                ["seasonLinkIdentifier"] = packet.seasonLinkIdentifier,
                ["weekendLinkIdentifier"] = packet.weekendLinkIdentifier,
                ["sessionLinkIdentifier"] = packet.sessionLinkIdentifier,
                ["pitStopWindowIdealLap"] = packet.pitStopWindowIdealLap,
                ["pitStopWindowLatestLap"] = packet.pitStopWindowLatestLap,
                ["pitStopRejoinPosition"] = packet.pitStopRejoinPosition,
                ["steeringAssist"] = packet.steeringAssist,
                ["brakingAssist"] = packet.brakingAssist,
                ["gearboxAssist"] = packet.gearboxAssist,
                ["pitAssist"] = packet.pitAssist,
                ["pitReleaseAssist"] = packet.pitReleaseAssist,
                ["ersAssist"] = packet.ersAssist,
                ["drsAssist"] = packet.drsAssist,
                ["dynamicRacingLine"] = packet.dynamicRacingLine,
                ["dynamicRacingLineType"] = packet.dynamicRacingLineType,
                ["gameMode"] = packet.gameMode.ToString(),
                ["ruleSet"] = packet.ruleSet.ToString(),
                ["timeOfDay"] = packet.timeOfDay,
                ["sessionlength"] = packet.sessionlength.ToString(),
                ["speedUnitsLeadPlayer"] = packet.speedUnitsLeadPlayer.ToString(),
                ["temperatureUnitsLeadPlayer"] = packet.temperatureUnitsLeadPlayer.ToString(),
                ["speedUnitsSecondaryPlayer"] = packet.speedUnitsSecondaryPlayer.ToString(),
                ["temperatureUnitsSecondaryPlayer"] = packet.temperatureUnitsSecondaryPlayer.ToString(),
                ["numSafetyCarPeriods"] = packet.numSafetyCarPeriods,
                ["numVirtualSafetyCarPeriods"] = packet.numVirtualSafetyCarPeriods,
                ["numRedFlagPeriods"] = packet.numRedFlagPeriods
            };
    
            // Add sessionData to gameData
            gameData["SessionData"] = sessionData;
        }

        private void Client_OnCarTelemetryDataReceive(CarTelemetryPacket packet)
        {
            int playerIndex = packet.header.playerCarIndex;

            CarTelemetryData carTelemetryData = packet.carTelemetryData[playerIndex];

            if (gameData == null)
            {
                gameData = new JObject();
            }

            gameData["CarTelemetryData"] = new JObject
            {
                ["engineRPM"] = carTelemetryData.engineRPM,
                ["speed"] = carTelemetryData.speed,
                ["throttle"] = carTelemetryData.throttle,
                ["steer"] = carTelemetryData.steer,
                ["brake"] = carTelemetryData.brake,
                ["clutch"] = carTelemetryData.clutch,
                ["gear"] = carTelemetryData.gear,
                ["drs"] = carTelemetryData.drs,
                ["revLightsPercent"] = carTelemetryData.revLightsPercent,
                ["revLightsBitValue"] = carTelemetryData.revLightsBitValue,
                ["brakesTemperature"] = new JArray(carTelemetryData.brakesTemperature),
                ["tyresSurfaceTemperature"] = new JArray(carTelemetryData.tyresSurfaceTemperature),
                ["tyresInnerTemperature"] = new JArray(carTelemetryData.tyresInnerTemperature),
                ["engineTemperature"] = carTelemetryData.engineTemperature,
                ["tyresPressure"] = new JArray(carTelemetryData.tyresPressure.Select(p => (double)p)),
                ["surfaceType"] = new JArray(carTelemetryData.surfaceType.Select(st => (int)st))
            };
        }
    }
}
