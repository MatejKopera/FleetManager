namespace FleetManager.Model
{
    public class TransportOrder
    {
        public int From { get; private set; }
        public int To { get; private set; }
        public int StartTime { get; private set; }
        public int VehicleId { get; set; }

        public TransportOrder(int from, int to, int startTime, int vehicleId)
        {
            From = from;
            To = to;
            StartTime = startTime;
            VehicleId = vehicleId;
        }
    }
}
