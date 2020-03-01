using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models.Responses
{
    public class LanguageChangedResponse
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public LanguageChangedParams Params { get; set; }
    }

    public partial class LanguageChangedParams
    {
        [JsonPropertyName("language_id")]
        public string LanguageId { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }
}
