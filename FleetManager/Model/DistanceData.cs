using System.Text.Json.Serialization;

namespace FleetManager.Model
{
    public class DistanceData
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }
}
