using System;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models.Responses
{
    public class NewViewResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}