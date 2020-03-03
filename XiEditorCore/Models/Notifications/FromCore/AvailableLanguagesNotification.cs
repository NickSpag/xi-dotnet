using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    /// <summary>
    /// Sent from the backend after ClientStartedNotification
    /// </summary>
    public class AvailableLanguageNotification
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("params")]
        public AvailableLanguageParams Params { get; set; }
    }

    public class AvailableLanguageParams
    {
        [JsonPropertyName("themes")]
        public List<string> Themes { get; set; }
    }
}
