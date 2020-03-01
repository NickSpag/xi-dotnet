using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{

    public class NewViewResponse
    {
        [JsonPropertyName("id")]
        public ushort Id { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }
    }

    public class AvailablePluginsResponse
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public AvailablePluginResponseParams Params { get; set; }
    }

    public class AvailablePluginResponseParams
    {
        [JsonPropertyName("plugins")]
        public List<object> Plugins { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }
}
