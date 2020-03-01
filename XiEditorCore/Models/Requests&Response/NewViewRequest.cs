using System.Text.Json.Serialization;

namespace XiEditorCore
{
    public class NewViewRequest
    {
        [JsonPropertyName("id")]
        public ushort Id { get; private set; }

        [JsonPropertyName("method")]
        public string Method => "new_view";

        [JsonPropertyName("params")]
        public NewViewParameters Params { get; set; } = new NewViewParameters();

        public NewViewRequest(ushort id)
        {
            this.Id = id;
        }
    }

    public class NewViewParameters
    {
        [JsonPropertyName("file_path")]
        string FilePath{ get; set; }
    }
}
