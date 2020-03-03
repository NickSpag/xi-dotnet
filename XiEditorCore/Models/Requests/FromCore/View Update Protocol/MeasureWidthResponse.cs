using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public class MeasureWidthResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("result")]
        public List<List<long>> Result { get; set; }
    }
}
