using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using DV;
using DV.Booklets;
using DV.Logic.Job;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityModManagerNet;
using Task = System.Threading.Tasks.Task;

namespace HomeSystemV2
{
    [EnableReloading]
    public class Class1
    {
        private static UnityModManager.ModEntry ModEntry;
        private static CancellationTokenSource cancellationTokenSource;

        // Public variables to store the latest train data and status
        public static Dictionary<string, Dictionary<string, object>> trainData = new Dictionary<string, Dictionary<string, object>>();
        public static float TrainSpeed;
        public static float AirPressure;
        public static bool IsStationary;
        public static bool IsDerailed;
        public static bool IsExploded;
        public static bool IsEligibleForSleep;
        public static float TrainTemps;
        public static float IndepBrakePos;
        public static float TrainBrakePos;
        public static float HandbrakePos;
        public static string TrainGUID;
        public static bool IsLoco;

        private static Dictionary<string, Dictionary<string, object>> prevTrainData;

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            ModEntry = modEntry;
            modEntry.OnUnload = Unload;

            StartLoop();

            Log("My Mod INIT? 3:11");

            return true;
        }

        static async Task StartLoop()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;


            while (!cancellationToken.IsCancellationRequested)
            {
                if (PlayerManager.Car != null)
                {
                    TrainGUID = PlayerManager.Car.ID;

                    trainData = new Dictionary<string, Dictionary<string, object>>();

                    foreach (TrainCar trainCar in PlayerManager.Car.trainset.cars)
                    {
                        Dictionary<string, object> carData = new Dictionary<string, object>();
                        carData["TrainGUID"] = trainCar.ID;

                        carData["FrontCoupler"] = new Dictionary<string, bool>()
                        {
                            { "AirCock", trainCar.frontCoupler.IsCockOpen }
                        };
                        carData["RearCoupler"] = new Dictionary<string, bool>()
                        {
                            { "AirCock", trainCar.rearCoupler.IsCockOpen }
                        };

                        trainData[trainCar.ID] = carData;
                    }

                    // Serialize trainData to JSON
                    string trainDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(trainData);

                    // Deserialize JSON to create a deep copy
                    prevTrainData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(trainDataJson);

                    IsLoco = PlayerManager.Car.IsLoco;

                    if (IsLoco)
                    {
                        TrainTemps = PlayerManager.Car.brakeSystem.heatController.temperature;
                        IndepBrakePos = PlayerManager.Car.brakeSystem.independentBrakePosition;
                        TrainBrakePos = PlayerManager.Car.brakeSystem.trainBrakePosition;
                        HandbrakePos = PlayerManager.Car.brakeSystem.handbrakePosition;
                    }

                    TrainSpeed = PlayerManager.Car.GetForwardSpeed().To1Decimal();
                    AirPressure = PlayerManager.Car.brakeSystem.brakeCylinderPressure.To1Decimal();
                    IsStationary = PlayerManager.Car.isStationary;
                    IsDerailed = PlayerManager.Car.derailed;
                    IsExploded = PlayerManager.Car.isExploded;
                    IsEligibleForSleep = PlayerManager.Car.isEligibleForSleep;

                    SendDataToWebSocket();
                }
                else
                {
                    LogWarning("PlayerManager.Car is null. Make sure it is properly initialized.");
                }
                await Task.Delay(5000);
            }
        }

        public static async Task SendDataToWebSocket()
        {
            try
            {
                // Locomotive data
                var locoData = new
                {
                    TrainSpeed,
                    AirPressure,
                    IsStationary,
                    IsDerailed,
                    IsExploded,
                    IsEligibleForSleep,
                    TrainTemps,
                    IndepBrakePos,
                    TrainBrakePos,
                    HandbrakePos,
                    TrainGUID,
                    IsLoco
                };

                var trainData = prevTrainData;

                JObject jsonDataObject = JObject.FromObject(new
                {
                    LocoData = locoData,
                    TrainData = trainData
                });

                jsonDataObject.Add("Type", "PutData");
                jsonDataObject.Add("Game", "DerailValley");

                using (ClientWebSocket webSocket = new ClientWebSocket())
                {
                    Uri serverUri = new Uri("ws://localhost:30151");
                    await webSocket.ConnectAsync(serverUri, default);

                    byte[] bytes = Encoding.UTF8.GetBytes(jsonDataObject.ToString());

                    await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, default);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                LogError($"Error sending data to WebSocket: {ex.Message}");
            }
        }
        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
            cancellationTokenSource?.Cancel();
            return true;
        }

        public static void AddressFloat(string name, float value)
        {
            unsafe
            {
                float* speedPtr = &value;
                IntPtr address = (IntPtr)speedPtr;

                Log($"Variable value ({name}) : {value}");
                Log($"Memory address: 0x{address.ToString("X")}");
            }
        }

        public static void Log(object msg)
        {
            WriteLog($"[Info] {msg}");
        }

        public static void LogWarning(object msg)
        {
            WriteLog($"[Warning] {msg}");
        }

        public static void LogError(object msg)
        {
            WriteLog($"[Error] {msg}");
        }

        private static void WriteLog(string msg)
        {
            string str = $"[{DateTime.Now:HH:mm:ss.fff}] {msg}";
            ModEntry.Logger.Log(str);
        }
    }
}
