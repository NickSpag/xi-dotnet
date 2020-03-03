using System;

namespace XiEditorCore.Models
{
    public interface IRPCRequest<T>
    {
        public ushort Id { get; set; }

        public string Method { get; set; }

        public T Params { get; set; }
    }
}
