using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{

    /// <summary>
    /// This is the notification object to let a client know it has started.
    /// </summary>
    /// <remarks>
    /// WARNING: If you do not set the ConfigDirector or the ClientExtrasDirectory, even to an empty path, core will not look for plugins.
    /// It's something about the difference between file-based configs or global configs? not sure.
    /// </remarks>
    public class ClientStartedNotification : IRPCNotification
    {
        [JsonIgnore]
        public string Method => Constants.XiMethods.ClientStarted;

        [JsonPropertyName("config_dir")]
        public string ConfigDirectory { get; set; } = "";

        [JsonPropertyName("client_extras_dir")]
        public string ClientExtrasDirectory { get; set; } = "";
    }
}
