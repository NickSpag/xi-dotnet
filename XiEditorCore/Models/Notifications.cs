using System;
using System.Dynamic;

namespace XiEditorCore
{
    public class GetConfigMessage
    {
        public ushort Id { get; set; }
        public string Method { get; set; } = "get_config";
        public StringParameters Parameters { get; set; }
    }

    public class StringParameters
    {
    }
}
