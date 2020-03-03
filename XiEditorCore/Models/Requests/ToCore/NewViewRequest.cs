using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public class NewViewRequest : IRPCRequest<NewViewParameters>
    {
        [JsonPropertyName("id")]
        public ushort Id { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; } = "new_view";

        [JsonPropertyName("params")]
        public NewViewParameters Params { get; set; } = new NewViewParameters();

        public NewViewRequest(ushort id)
        {
            Id = id;
        }
    }

    public class NewViewParameters
    {
        [JsonPropertyName("file_path")]
        string FilePath{ get; set; }
    }
}
