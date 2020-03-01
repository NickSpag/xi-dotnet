using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models.Responses
{
    /// <summary>
    /// Sent from the backend after ClientStartedNotification
    /// </summary>
    public class AvailableLanguageResponse
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("params")]
        public AvailableLanguageResponseParams Params { get; set; }
    }

    public class AvailableLanguageResponseParams
    {
        [JsonPropertyName("themes")]
        public List<string> Themes { get; set; }
    }
}
