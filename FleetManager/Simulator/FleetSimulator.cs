using FleetManager.Model;
using FleetManager.Strategy;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.Simulator
{
    public class FleetSimulator
    {
        public int AgvCount { get; set; }
        public int TotalDurationTime { get; set; }
        public List<TaktTime> TaktTimes { get; set; }
        public List<DistanceData> Distances { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<TransportOrder> TransportOrders { get; } = new List<TransportOrder>();
        public int CurrentTime { get; set; } = 0;

        public int TotalIdleTime => Vehicles.Sum(x => x.IdleTime);
        public int MinVehiclesUsed => TransportOrders.Max(x => x.VehicleId);
        public int PenaltyTime { get; set; }

        public void Simulate(IStrategy strategy)
        {
            PenaltyTime = strategy.Execute();
        }
    }
}
