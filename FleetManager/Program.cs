using FleetManager.Simulator;
using FleetManager.Strategy;
using System;
using System.Linq;

namespace FleetManager
{
    internal class Program
    {
        private const string _inputFile = "Data/sample.json";
        private const string _outputFile = "Data/out.json";

        static void Main(string[] args)
        {
            FleetSimulator fleetSimulator = JsonReader.LoadInput(_inputFile);

            if (fleetSimulator != null)
            {
                IStrategy strategy = new OptimalStrategy(fleetSimulator.Vehicles, fleetSimulator.TransportOrders, fleetSimulator.TaktTimes, fleetSimulator.Distances, fleetSimulator.TotalDurationTime);
                fleetSimulator.Simulate(strategy);

                ShowResults(fleetSimulator);

                JsonWriter.SaveJson(_outputFile, fleetSimulator);
            }
            else
            {
                Console.WriteLine("Failed to load data.");
            }
        }

        private static void ShowResults(FleetSimulator fleetSimulator)
        {
            Console.WriteLine();
            Console.WriteLine("---RESULTS---");
            //foreach (var transportOrder in fleetSimulator.TransportOrders.OrderBy(x => x.StartTime))
            //{
            //    Console.WriteLine($"time: {transportOrder.StartTime} agv:{transportOrder.VehicleId} from:{transportOrder.From} to:{transportOrder.To}");
            //}
            Console.WriteLine($"Idle Time: {fleetSimulator.TotalIdleTime}");
            Console.WriteLine($"Penalty Time: {fleetSimulator.PenaltyTime}");
            Console.WriteLine($"Minimum AGV Count: {fleetSimulator.MinVehiclesUsed}");
            Console.WriteLine($"Transport Orders Count: {fleetSimulator.TransportOrders.Count}");
            Console.WriteLine();
            foreach (var vehicle in fleetSimulator.Vehicles)
            {
                Console.WriteLine($"Agv Id: {vehicle.Id} Idle time: {vehicle.IdleTime} Transport orders: {fleetSimulator.TransportOrders.Count(x => x.VehicleId == vehicle.Id)}");
            }
        }
    }
}
