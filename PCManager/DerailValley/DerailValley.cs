using System.Threading;
using Swed64;

namespace DerailValley
{
    public partial class DerailValley
    {

        private readonly HttpClient httpClient;

        public DerailValley()
        {
            httpClient = new HttpClient();
        }

        public async Task UpdateData()
        {
            try
            {
                string url = "http://localhost:30152/";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseData);
                }
                else
                {
                    Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        
        private bool isRunning;
        
        
        public void Start()
        {
        }
    
        
        public void End()
        {
            isRunning = false;
        }
    }
}