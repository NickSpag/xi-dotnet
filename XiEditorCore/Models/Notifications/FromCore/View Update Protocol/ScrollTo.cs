using System;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models.Responses
{
    public partial class ScrollTo
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public ScrollToParams Params { get; set; }
    }

    public partial class ScrollToParams
    {
        [JsonPropertyName("col")]
        public long Column { get; set; }

        [JsonPropertyName("line")]
        public long Line { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }
}
