using System.Text.Json.Serialization;

namespace FleetManager.Model
{
    public class TransportOrderDto
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("agv")]
        public int Agv { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }
    }
}