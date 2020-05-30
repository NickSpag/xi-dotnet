using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using static XiEditorCore.Constants;

namespace XiEditorCore
{
    public class RustCore : IDisposable
    {
        RustCoreHandle _rustCore;

        private string GetPluginsDirectory() => Path.Combine(Directory.GetCurrentDirectory(), XiCoreStructure.PluginDirectory);
        private string GetConfigDirectory() => Path.Combine(Directory.GetCurrentDirectory(), XiCoreStructure.ConfigDirectory);
        //private string GetXiCoreBinaryPath() => Path.Combine(Directory.GetCurrentDirectory(), XiCoreStructure.ExecutableName);

        public List<RustView> Views { get; }

        public RustCore()
        {
            Views = new List<RustView>();

            _rustCore = RustBindings.CreateCore(GetConfigDirectory(), GetPluginsDirectory());
        }

        public void CreateView()
        {
            Views.Add(new RustView(this));
        }

        public void Dispose()
        {
            _rustCore.Dispose();
        }
    }

    public class RustCoreHandle : SafeHandle
    {

        public RustCoreHandle() : base(IntPtr.Zero, true)
        { }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            if(!IsInvalid)
            {
                RustBindings.FreeView(handle);
            }

            return true;
        }
    }
}
