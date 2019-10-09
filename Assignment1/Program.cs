using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new RealTimeCityBikeDataFetcher();
            var stationName = "Petikontie";
            var onlineOrOffline = "realtime";

            if (args.Length > 0)
            {
                stationName = args[0];
                if (args.Length == 2)
                {
                    onlineOrOffline = args[1];
                }
            }

            try
            {
                if (onlineOrOffline == "realtime")
                {
                    var cnt = x.GetBikeCountInStation(stationName);
                    Console.WriteLine(cnt.Result);
                }
                else
                {
                    var cnt = x.GetBikeCountInStationOffline(stationName);
                    Console.WriteLine(cnt.Result);
                }

            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();

        }
    }
}
