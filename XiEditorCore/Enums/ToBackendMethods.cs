using System;

namespace XiEditorCore.Enums
{
    /// <summary>
    /// Requests sent to backend from the frontend, expecting a response.
    /// </summary>
    public enum ToBackendRequest
    {
        NewView,    //expects view id like "view-id-1"
        GetConfig,  // Expects config Object
        Edit, // only for copy/cut
    }

    /// <summary>
    /// Notifications sent to backend. 
    /// </summary>
    public enum ToBackendNotification
    {
        ClientStarted,
        CloseView,
        Save,
        SetTheme,
        SetLanguage,
        ModifyUserConfig,
        Edit, // everything else besides copy/cut
    }

    //todo figure out find and replace, plugins, recording.
}