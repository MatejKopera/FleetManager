using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FleetManager.Model
{
    public class InputDataModel
    {
        [JsonPropertyName("agvCount")]
        public int AgvCount { get; set; }

        [JsonPropertyName("totalDurationTime")]
        public int TotalDurationTime { get; set; }

        [JsonPropertyName("taktTimes")]
        public List<TaktTime> TaktTimes { get; set; }

        [JsonPropertyName("distances")]
        public List<DistanceData> Distances { get; set; }
    }
}
