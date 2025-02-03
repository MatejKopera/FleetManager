namespace FleetManager.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int AvailableAt { get; set; } = 0;
        public int IdleTime { get; set; } = 0;

        public Vehicle(int id)
        {
            Id = id;
        }
    }
}
