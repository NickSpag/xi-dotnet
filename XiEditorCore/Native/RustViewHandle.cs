using System;
using System.Runtime.InteropServices;
using System.Text;

namespace XiEditorCore
{
    //todo this would be the new XiView, basically
    public class RustView : IDisposable
    {
        RustViewHandle _rustView;

        public RustView(RustCore core = null)
        {

            _rustView = RustBindings.CreateView();
        }

        public bool HasValidHandle => !_rustView.IsInvalid;

        public void Insert(string text)
        {
            RustBindings.Insert(_rustView, text);
        }

        public void Dispose()
        {
            _rustView.Dispose();
        }
    }

    public class RustViewHandle : SafeHandle
    {
        public RustViewHandle() : base(IntPtr.Zero, true)
        { }

        public override bool IsInvalid => handle == IntPtr.Zero;

        public string AsString()
        {
            int len = 0;

            while (Marshal.ReadByte(handle, len) != 0)
            {
                len++;
            }

            byte[] buffer = new byte[len];

            Marshal.Copy(handle, buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(buffer);
        }

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                RustBindings.FreeView(handle);
            }

            return true;
        }
    }
}
