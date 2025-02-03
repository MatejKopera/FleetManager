using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FleetManager.Simulator
{
    public static class JsonReader
    {
        public static FleetSimulator LoadInput(string filename)
        {
            var inputData = LoadJson<InputDataModel>(filename);

            if (inputData != null)
            {
                Console.WriteLine("Reading input file...");
                Console.WriteLine($"AGV Count in Input: {inputData.AgvCount}");
                Console.WriteLine($"Total Duration Time in Input: {inputData.TotalDurationTime}");
                Console.WriteLine($"Takt Times in Input: {inputData.TaktTimes.Count}");
                Console.WriteLine("Input Json read successfully.");

                foreach (var taktTime in inputData.TaktTimes)
                {
                    // init first takt
                    taktTime.NextTaktTime = taktTime.Time;
                }

                return new FleetSimulator
                {
                    AgvCount = inputData.AgvCount,
                    TotalDurationTime = inputData.TotalDurationTime,
                    Distances = inputData.Distances,
                    TaktTimes = inputData.TaktTimes,
                    Vehicles = InitializeVehicles(inputData.AgvCount),
                };
            }

            return null;
        }

        private static List<Vehicle> InitializeVehicles(int agvCount)
        {
            return Enumerable.Range(1, agvCount).Select(id => new Vehicle(id)).ToList();
        }

        private static T LoadJson<T>(string filename)
        {
            try
            {
                string jsonString = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename));
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File '{filename}' not found.");
                return default;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return default;
            }
        }
    }
}
