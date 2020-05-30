using System;
using System.Runtime.InteropServices;

namespace XiEditorCore
{
    public static class RustBindings
    {
        private const string NativeLibrary = "/Users/nickspagnola/Projects/PInvokeTesting/rust/target/release/lib_ffi_test.dylib";

        [DllImport(NativeLibrary, EntryPoint = "create_view", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern RustViewHandle _create_view();

        [DllImport(NativeLibrary, EntryPoint = "free_view", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern void _free_view(IntPtr view);

        [DllImport(NativeLibrary, EntryPoint = "insert", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern void _insert(RustViewHandle view, string text);

        [DllImport(NativeLibrary, EntryPoint = "create_core", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern RustCoreHandle _create_core(string configDir, string clientExtrasDir);


        public static RustCoreHandle CreateCore(string configDir, string clientExtrasDir) => _create_core(configDir, clientExtrasDir);

        public static void Insert(RustViewHandle view, string text) => _insert(view, text);

        public static void FreeView(IntPtr viewHandle) => _free_view(viewHandle);

        public static RustViewHandle CreateView() => _create_view();

    }
}
