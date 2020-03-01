using System;
namespace XiEditorCore.Enums
{
    /// <summary>
    /// Methods the frontend can call on the backend
    /// </summary>
    public enum BackendRequestMethods
    {
        ClientStarted, //sent without an ID
        NewView,
        CloseView,
        Save,
        SetTheme,
        SetLanguage,
        ModifyUserConfig,
        GetConfig,
    }

    public enum BackendFrontendRequestMethods
    {
        DefStyle,
        ScrollTo,
        Update,
        MeasureWidth,
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
    }
}