using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models.Responses
{
    public class ConfigChangedResponse
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public ConfigChangedResponseParams Params { get; set; }
    }

    public class ConfigChangedResponseParams
    {
        [JsonPropertyName("changes")]
        public Changes Changes { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }
    }

    public partial class Changes
    {
        [JsonPropertyName("auto_indent")]
        public bool AutoIndent { get; set; }

        [JsonPropertyName("autodetect_whitespace")]
        public bool AutodetectWhitespace { get; set; }

        [JsonPropertyName("font_face")]
        public string FontFace { get; set; }

        [JsonPropertyName("font_size")]
        public long FontSize { get; set; }

        [JsonPropertyName("line_ending")]
        public string LineEnding { get; set; }

        [JsonPropertyName("plugin_search_path")]
        public List<object> PluginSearchPath { get; set; }

        [JsonPropertyName("save_with_newline")]
        public bool SaveWithNewline { get; set; }

        [JsonPropertyName("scroll_past_end")]
        public bool ScrollPastEnd { get; set; }

        [JsonPropertyName("surrounding_pairs")]
        public List<List<string>> SurroundingPairs { get; set; }

        [JsonPropertyName("tab_size")]
        public long TabSize { get; set; }

        [JsonPropertyName("translate_tabs_to_spaces")]
        public bool TranslateTabsToSpaces { get; set; }

        [JsonPropertyName("use_tab_stops")]
        public bool UseTabStops { get; set; }

        [JsonPropertyName("word_wrap")]
        public bool WordWrap { get; set; }

        [JsonPropertyName("wrap_width")]
        public long WrapWidth { get; set; }
    }

}
