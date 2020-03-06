using System;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public partial class ScrollToNotification
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public ScrollToParams Params { get; set; }
    }

    public partial class ScrollToParams
    {
        [JsonPropertyName("col")]
        public int Column { get; set; }

        [JsonPropertyName("line")]
        public int Line { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }
}
