using System;

namespace XiEditorCore
{
    public static partial class Constants
    {
        public static class XiCoreStructure
        {
            public const string ExecutableName = "xi-core-unix";
            public const string DebugExecutablePath = "/Users/nickspagnola/Projects/XiEditor/XiEditorCore/Resources/xi-core-unix";

            public const string ConfigDirectory = "config/";

            public const string PluginDirectory = "plugins/";
            public const string SyntectPluginDirectory = PluginDirectory + "syntect/";
            public const string SyntectBinaryDirectory = SyntectPluginDirectory + "bin/";
            public const string SyntectBinaryPath = SyntectBinaryDirectory + SyntectBinaryName;
            public const string SyntectBinaryName = "xi-syntect-plugin";

            public const string SyntectManifestPath = SyntectPluginDirectory + SyntectManifestName;
            public const string SyntectManifestName = "manifest.toml";

        }
    }
}
