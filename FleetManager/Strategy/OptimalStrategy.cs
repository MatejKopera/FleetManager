using FleetManager.Model;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.Strategy
{
    public class OptimalStrategy : IStrategy
    {
        private const int _depo = 0;

        public List<Vehicle> Vehicles { get; set; }
        public List<TransportOrder> TransportOrders { get; }
        public List<TaktTime> TaktTimes { get; set; }
        public List<DistanceData> Distances { get; set; }
        public int TotalDurationTime { get; set; }
        public int PenaltyTime { get; private set; }

        public OptimalStrategy(
            List<Vehicle> vehicles,
            List<TransportOrder> transportOrders,
            List<TaktTime> taktTimes,
            List<DistanceData> distances,
            int totalDurationTime)
        {
            Vehicles = vehicles;
            TransportOrders = transportOrders;
            TaktTimes = taktTimes;
            Distances = distances;
            TotalDurationTime = totalDurationTime;
        }

        public int Execute()
        {
            while (true)
            {
                // find next takt time
                TaktTime taktTime = TaktTimes
                    .Where(x => x.NextTaktTime <= TotalDurationTime)
                    .OrderBy(x => x.NextTaktTime)
                    .FirstOrDefault();

                if (taktTime == null)
                {
                    UpdateIdleTimeAtTheEnd();
                    return PenaltyTime;
                }

                int fromDepoStartTime;
                var distanceFromDepo = GetDistance(_depo, taktTime.From);

                // find first available vehicle
                var vehicle = Vehicles.FirstOrDefault(x => x.AvailableAt + distanceFromDepo <= taktTime.NextTaktTime);

                if (vehicle != null)
                {
                    fromDepoStartTime = taktTime.NextTaktTime - distanceFromDepo;
                    vehicle.IdleTime += fromDepoStartTime - vehicle.AvailableAt;
                }
                else
                {
                    // no availabe vehicle could arrive on time
                    // note: no idle time, dispatch immediately after any arrive to depo
                    vehicle = Vehicles.OrderBy(x => x.AvailableAt).First();

                    fromDepoStartTime = vehicle.AvailableAt;
                }

                // pick up and plan next
                var pickUpTime = fromDepoStartTime + distanceFromDepo;

                // dispatch
                var distanceToDestination = GetDistance(taktTime.From, taktTime.To);
                var arrivalTime = pickUpTime + distanceToDestination; ;

                // return to depo
                var distanceToDepo = GetDistance(taktTime.To, _depo);
                vehicle.AvailableAt = arrivalTime + distanceToDepo;

                CreateTransportOrder(_depo, taktTime.From, fromDepoStartTime, vehicle.Id, pickUpTime);
                CreateTransportOrder(taktTime.From, taktTime.To, pickUpTime, vehicle.Id, arrivalTime);
                CreateTransportOrder(taktTime.To, _depo, arrivalTime, vehicle.Id, vehicle.AvailableAt);

                int penalty = fromDepoStartTime + distanceFromDepo - taktTime.NextTaktTime;
                PenaltyTime += penalty;

                taktTime.NextTaktTime += taktTime.Time;
            }
        }

        private void UpdateIdleTimeAtTheEnd()
        {
            foreach (var vehicle in Vehicles)
            {
                if (TransportOrders.Select(x => x.VehicleId).Contains(vehicle.Id)
                    && vehicle.AvailableAt <= TotalDurationTime)
                {
                    vehicle.IdleTime += TotalDurationTime - vehicle.AvailableAt;
                }
            }
        }


        private void CreateTransportOrder(int from, int to, int startTime, int vehicleId, int endTime)
        {
            if (endTime <= TotalDurationTime)
            {
                TransportOrders.Add(new TransportOrder(from, to, startTime, vehicleId));
            }
        }

        private int GetDistance(int from, int to)
        {
            var distanceData = Distances.FirstOrDefault(
                x => (x.From == from && x.To == to)
                  || (x.From == to && x.To == from));

            return distanceData?.Distance ?? -1;
        }
    }
}
