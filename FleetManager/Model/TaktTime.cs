using System.Text.Json.Serialization;

namespace FleetManager.Model
{
    public class TaktTime
    {
        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }

        public int NextTaktTime { get; set; }
    }
}
