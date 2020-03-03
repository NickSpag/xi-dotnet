using System;
using System.Text.Json.Serialization;

namespace XiEditorCore.Models
{
    public interface IRPCNotification
    {
        public string Method { get; }
    }
}
