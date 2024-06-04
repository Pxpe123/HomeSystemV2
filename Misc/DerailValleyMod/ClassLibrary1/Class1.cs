using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DV;
using UnityEngine;
using UnityModManagerNet;

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

            // Start the HTTP server to listen on port 30152
            Task.Run(() => StartRestApiServer(cancellationToken));

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

                    // Check if trainData has changed
                    if (trainDataJson != Newtonsoft.Json.JsonConvert.SerializeObject(prevTrainData))
                    {
                        Log($"Train Data: {trainDataJson}");
                    }
                }
                else
                {
                    LogWarning("PlayerManager.Car is null. Make sure it is properly initialized.");
                }

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

                await Task.Delay(5000);
            }
        }

        private static void StartRestApiServer(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://*:30152/");
                listener.Start();

                Log("HTTP Server started on port 30152");

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var context = listener.GetContext();
                        var response = context.Response;

                        string responseString = RESTGetData();
                        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                        response.ContentLength64 = buffer.Length;
                        var output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                    catch (Exception ex)
                    {
                        LogError($"Error in HTTP server: {ex.Message}");
                    }
                }

                listener.Stop();
            });
        }

        public static string RESTGetData()
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

            // Train car data
            var trainData = prevTrainData;

            // Serialize locomotive data to JSON
            string locoDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(locoData);

            // Serialize train car data to JSON
            string trainDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(trainData);

            // Combine the JSON strings into a single JSON object
            string jsonData = $"{{\"LocoData\": {locoDataJson}, \"TrainData\": {trainDataJson}}}";

            return jsonData;
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
