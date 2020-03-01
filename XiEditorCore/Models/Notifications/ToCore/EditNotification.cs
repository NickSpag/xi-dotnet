using System.Text.Json.Serialization;

namespace XiEditorCore.Models.Notifications.ToCore
{
    public class EditNotification
    {
        [JsonPropertyName("method")]
        public string Method => "edit";

        [JsonPropertyName("params")]
        public EditParameters Parameters { get; set; }

        public EditNotification(string viewId, char @char)
        {
            Parameters = new EditParameters(viewId, @char);
        }
    }

    public class EditParameters
    {
        [JsonPropertyName("method")]
        public string Method => "insert";

        [JsonPropertyName("params")]
        public InsertParameters Parameters { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }

        public EditParameters(string viewId, char @char)
        {
            ViewId = viewId;
            Parameters = new InsertParameters(@char);
        }
    }

    public class InsertParameters
    {
        [JsonPropertyName("chars")]
        public char Chars { get; set; }

        public InsertParameters(char @char)
        {
            Chars = @char;
        }
    }
}
