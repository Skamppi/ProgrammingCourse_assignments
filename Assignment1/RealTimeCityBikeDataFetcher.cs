using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace Assignment1
{
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public Task<int> GetBikeCountInStation(string stationName)
        {
            // check if name contains number
            bool containsNumber = stationName.Any(char.IsDigit);
            if (containsNumber)
            {
                throw new Exception($"Invalid argument: '{stationName}'");
            }

            string url = "https://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
            HttpClient Client = new HttpClient();
            var response = Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;

                // by calling .Result you are synchronously reading the result
                string responseString = responseContent.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<StationRoot>(responseString);
                // find station from list
                var station = model.stations.SingleOrDefault(p => p.name == stationName);
                // did it exist?
                if (station != null)
                {
                    return Task.FromResult(station.bikesAvailable);
                }
                else
                {
                    throw new NotFoundException($"Not found: '{stationName}'");
                }
            }
            else
            {
                throw new Exception("Api call failed");
            }

        }
        public Task<int> GetBikeCountInStationOffline(string stationName)
        {
            // check if name contains number
            bool containsNumber = stationName.Any(char.IsDigit);
            if (containsNumber)
            {
                throw new Exception($"Invalid argument: '{stationName}'");
            }

            // read lines from file
            bool found = false;
            string cnt = "";
            string[] lines = System.IO.File.ReadAllLines(@"bikedata.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                // split line: stationName and count
                string[] val = lines[i].Split(':');
                // if name is what we are looking for
                if (val[0].Trim() == stationName)
                {
                    found = true;
                    cnt = val[1].Trim(); //get bike count
                    break; // stop for loop
                }
            }

            if (found == false)
            {
                throw new NotFoundException($"Not found: '{stationName}'");
            }

            return Task.FromResult(int.Parse(cnt));
        }
    }
}
