using System;

namespace XiEditorCore
{
    public enum ToFrontendRequests
    {
        //https://xi-editor.io/xi-editor/docs/frontend-protocol.html#measure_width
        MeasureWidth, //expects {"id":0, "result":[[28.0,8.0]]}
    }

    public enum ToFrontendNotifications
    {
        ThemeChanged,
        AvailableThemes,
        LanguageChanged,
        AvailableLanguages,
        ConfigChanged,
        AvailablePlugins,
        PluginStarted,
        PluginStopped,
        UpdateCmds,
        UpdateSpans,
        UpdateAnnotations,
        ShowHover,
        AddStatusItem,
        UpdateStatusItem,
        RemoveStatusItem,
        FindStatus,
        ReplaceStatus,

        //View update protocol- the methods related to the core telling the front end to update
        DefStyle,
        ScrollTo,
        Update,
    }
}
