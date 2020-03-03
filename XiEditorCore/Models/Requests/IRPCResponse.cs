using System;
using System.Text.Json.Serialization;

namespace XiEditorCore
{
    public interface IRPCResponse<T>
        where T : class
    {
        public string Id { get; set; }

        public T Result { get; set; }
    }
}