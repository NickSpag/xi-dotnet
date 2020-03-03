using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    /// <summary>
    /// Received in Response to NewViewRequest
    /// </summary>
    public partial class AvailablePluginsNotification
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public AvailablePluginsParams Params { get; set; }
    }

    public partial class AvailablePluginsParams
    {
        [JsonPropertyName("plugins")]
        public List<string> Plugins { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }
}
