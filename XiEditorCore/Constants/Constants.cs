using System;

namespace XiEditorCore
{
    public static partial class Constants
    {
        public static partial class BackendRequestMethods
        {
            /// <summary>
            /// General Operations
            /// </summary>
            public const string ClientStarted = "client_started";
            public const string NewView = "new_view";
            public const string CloseView = "close_view";
            public const string Save = "save";
            public const string SetTheme = "set_theme";
            public const string SetLanguage = "set_language";
            public const string ModifyUserConfig = "modify_user_config";
            public const string GetConfig = "get_config";

            #region Edit Methods
            /// <summary>
            /// Parameterless General Edit Operations
            /// </summary>
            public const string DeleteBackward = "delete_backward";
            public const string DeleteForward = "delete_forward";
            public const string DeleteWordForward = "delete_word_forward";
            public const string DeleteWordBackward = "delete_word_backward";
            public const string DeleteToEndOfParagraph = "delete_to_end_of_paragraph";
            public const string DeleteToBeginningOfLine = "delete_to_beginning_of_line";
            public const string InsertNewline = "insert_newline";
            public const string InsertTab = "insert_tab";
            public const string DuplicateLine = "duplicate_line";
            public const string MoveUp = "move_up";
            public const string MoveUpAndModifySelection = "move_up_and_modify_selection";
            public const string MoveDown = "move_down";
            public const string MoveDownAndModifySelection = "move_down_and_modify_selection";
            public const string MoveLeft = "move_left";
            public const string MoveLeftAndModifySelection = "move_left_and_modify_selection";
            public const string MoveRight = "move_right";
            public const string MoveRightAndModifySelection = "move_right_and_modify_selection";
            public const string MoveWordLeft = "move_word_left";
            public const string MoveWordLeftAndModifySelection = "move_word_left_and_modify_selection";
            public const string MoveWordRight = "move_word_right";
            public const string MoveWordRightAndModifySelection = "move_word_right_and_modify_selection";
            public const string MoveToBeginningOfParagraph = "move_to_beginning_of_paragraph";
            public const string MoveToBeginningOfParagraphAndModifySelection = "move_to_beginning_of_paragraph_and_modify_selection";
            public const string MoveToEndOfParagraph = "move_to_end_of_paragraph";
            public const string MoveToEndOfParagraphAndModifySelection = "move_to_end_of_paragraph_and_modify_selection";
            public const string MoveToLeftEndOfLine = "move_to_left_end_of_line";
            public const string MoveToLeftEndOfLineAndModifySelection = "move_to_left_end_of_line_and_modify_selection";
            public const string MoveToRightEndOfLine = "move_to_right_end_of_line";
            public const string MoveToRightEndOfLineAndModifySelection = "move_to_right_end_of_line_and_modify_selection";
            public const string MoveToBeginningOfDocument = "move_to_beginning_of_document";
            public const string MoveToBeginningOfDocumentAndModifySelection = "move_to_beginning_of_document_and_modify_selection";
            public const string MoveToEndOfDocument = "move_to_end_of_document";
            public const string MoveToEndOfDocumentAndModifySelection = "move_to_end_of_document_and_modify_selection";
            public const string ScrollPageUp = "scroll_page_up";
            public const string PageUpAndModifySelection = "page_up_and_modify_selection";
            public const string ScrollPageDown = "scroll_page_down";
            public const string PageDownAndModifySelection = "page_down_and_modify_selection";
            public const string Yank = "yank";
            public const string Transpose = "transpose";
            public const string SelectAll = "select_all";
            public const string CollapseSelections = "collapse_selections";
            public const string AddSelectionAbove = "add_selection_above";
            public const string AddSelectionBelow = "add_selection_below";
            public const string Undo = "undo";
            public const string Redo = "redo";

            /// <summary>
            /// Text Transformation Methods
            /// </summary>
            public const string Uppercase = "uppercase";
            public const string Lowercase = "lowercase";
            public const string Capitalize = "capitalize";
            public const string Indent = "indent";
            public const string Outdent = "outdent";

            public const string IncreaseNumber = "increase_number";
            public const string DecreaseNumber = "decrease_number";

            /// <summary>
            /// Recording Methods
            /// </summary>
            public const string PlayRecording = "play_recording";
            public const string ToggleRecording = "toggle_recording";
            public const string ClearRecording = "clear_recording";

            /// <summary>
            /// Language Support Oriented Features (frontend -> backend)
            /// </summary>
            public const string RequestHover = "request_hover";
            #endregion

            #region Plugin Methods

            #endregion

            #region Find and Replace Methods
            public const string Find = "find";
            public const string MultiFind = "multi_find";
            public const string FindNext = "find_next";
            public const string FindPrevious = "find_previous";
            public const string FindAll = "find_all";
            public const string HighlightFind = "highlight_find";
            public const string SelectionForFind = "selection_for_find";
            public const string Replace = "replace";
            public const string SelectionForReplace = "selection_for_replace";
            public const string ReplaceNext = "replace_next";
            public const string ReplaceAll = "replace_all";
            public const string SelectionIntoLines = "selection_into_lines";
            #endregion

            #region Backend-to-Frontend Methods
            public const string DefStyle = "def_style";
            public const string ScrollTo = "scroll_to";
            public const string Update = "update";
            public const string MeasureWidth = "measure_width";
            public const string ThemeChanged = "theme_changed";
            public const string AvailableThemes = "available_themes";
            public const string LanguageChanged = "language_changed";
            public const string AvailableLanguages = "available_languages";
            public const string ConfigChanged = "config_changed";
            public const string AvailablePlugins = "available_plugins";
            public const string PluginStarted = "plugin_started";
            public const string PluginStopped = "plugin_stopped";
            public const string UpdateCmds = "update_cmds";
            public const string UpdateSpans = "update_spans";
            public const string UpdateAnnotations = "update_annotations";
            public const string ShowHover = "show_hover";
            public const string AddStatusItem = "add_status_item";
            public const string UpdateStatusItem = "update_status_item";
            public const string RemoveStatusItem = "remove_status_item";
            public const string FindStatus = "find_status";
            public const string ReplaceStatus = "replace_status";
            #endregion
        }
    }
}
