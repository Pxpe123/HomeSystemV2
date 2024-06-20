using System;
using System.Threading;
using Newtonsoft.Json.Linq;
using Swed64;

namespace DerailValley
{
    public class DerailValley
    {
        public JObject GameData { get; private set; }

        public void SetGameData(JToken gameData)
        {
            // Ensure gameData is of type JObject before casting
            if (gameData is JObject obj)
            {
                GameData = obj;
            }
            else
            {
                Console.WriteLine("Received game data is not of type JObject.");
            }
        }

        public JObject GetGameData()
        {
            return GameData;
        }
    }
}