using System.Text.Json.Serialization;

namespace XiEditorCore
{
    public class ClientStartedNotification
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = "client_started";

        [JsonPropertyName("params")]
        public ClientStartedParameters Params { get; set; } = new ClientStartedParameters();
    }

    public class ClientStartedParameters
    {
        [JsonPropertyName("config_dir")]
        public string ConfigDirectory { get; set; }

        [JsonPropertyName("client_extras_dir")]
        public string ClientExtrasDirectory { get; set; }
    }
}
