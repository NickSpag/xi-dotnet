using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public class UpdateNotification
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public UpdateParams Params { get; set; }
    }

    public partial class UpdateParams
    {
        [JsonPropertyName("update")]
        public Update Update { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }

    public partial class Update
    {
        [JsonPropertyName("annotations")]
        public List<Annotation> Annotations { get; set; }

        [JsonPropertyName("ops")]
        public List<Operation> Operations { get; set; }

        /// <summary>
        /// Whether or not the document, after this update, has unsaved changes
        /// </summary>
        [JsonPropertyName("pristine")]
        public bool Pristine { get; set; }
    }

    public partial class Annotation
    {
        /// <summary>
        /// Number of Ranges
        /// </summary>
        [JsonPropertyName("n")]
        public long N { get; set; }

        /// <summary>
        /// Can be any json object or value
        /// </summary>
        [JsonPropertyName("payloads")]
        public object Payloads { get; set; }

        // start_line, start_col, end_line, end_col todo tuple?
        [JsonPropertyName("ranges")]
        public List<List<ushort>> Ranges { get; set; }

        // "find" | "selection" | ...
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public partial class Operation
    {
        [JsonPropertyName("lines")]
        public List<Line> Lines { get; set; }

        [JsonPropertyName("n")]
        public long N { get; set; }

        /// <summary>
        /// "copy" | "skip" | "invalidate" | "update" | "ins"
        /// </summary>
        [JsonPropertyName("op")]
        public string Identifier { get; set; }
    }

    public partial class Line
    {
        [JsonPropertyName("cursor")]
        public List<long> Cursor { get; set; }

        [JsonPropertyName("ln")]
        public long Ln { get; set; }

        [JsonPropertyName("styles")]
        public List<object> Styles { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
