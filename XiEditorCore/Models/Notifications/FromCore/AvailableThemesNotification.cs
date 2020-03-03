using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public class AvailableThemesNotification
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("params")]
        public AvailableThemesNotificationParams Params { get; set; }
    }

    public class AvailableThemesNotificationParams
    {
        [JsonPropertyName("themes")]
        public List<string> Themes { get; set; }
    }
}
