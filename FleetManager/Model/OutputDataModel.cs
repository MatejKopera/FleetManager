using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FleetManager.Model
{
    public class OutputDataModel
    {
        [JsonPropertyName("transportOrders")]
        public List<TransportOrderDto> TransportOrders { get; set; }

        [JsonPropertyName("idleTime")]
        public int IdleTime { get; set; }

        [JsonPropertyName("penaltyTime")]
        public int PenaltyTime { get; set; }

        [JsonPropertyName("minimumAgvCount")]
        public int MinimumAgvCount { get; set; }
    }
}
