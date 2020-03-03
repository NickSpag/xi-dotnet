using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public class MeasureWidthRequest
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("strings")]
        public List<string> Strings { get; set; }
    }
}
