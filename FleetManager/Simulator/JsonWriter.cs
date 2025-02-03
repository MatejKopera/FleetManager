using FleetManager.Model;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FleetManager.Simulator
{
    public static class JsonWriter
    {
        public static void SaveJson(string filename, FleetSimulator fleetSimulator)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            try
            {
                var output = new OutputDataModel
                {
                    TransportOrders = fleetSimulator.TransportOrders.Select(transportOrder => new TransportOrderDto
                    {
                        Time = transportOrder.StartTime,
                        Agv = transportOrder.VehicleId,
                        From = transportOrder.From,
                        To = transportOrder.To
                    }).ToList(),
                    IdleTime = fleetSimulator.TotalIdleTime,
                    PenaltyTime = fleetSimulator.PenaltyTime,
                    MinimumAgvCount = fleetSimulator.MinVehiclesUsed
                };

                string jsonString = JsonSerializer.Serialize(output, options);
                File.WriteAllText(filename, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
