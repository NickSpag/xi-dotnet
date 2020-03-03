using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public class NewViewResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}