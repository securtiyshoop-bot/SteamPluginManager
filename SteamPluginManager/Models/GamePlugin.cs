using Newtonsoft.Json;

namespace SteamPluginManager.Models
{
    public class GamePlugin
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("appId")]
        public int AppId { get; set; }

        [JsonProperty("executable")]
        public string Executable { get; set; }

        [JsonProperty("launchPath")]
        public string LaunchPath { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("banner")]
        public string Banner { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("developer")]
        public string Developer { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        public GamePlugin()
        {
            Enabled = true;
            Tags = new string[] { };
            Version = "1.0";
        }
    }
}
