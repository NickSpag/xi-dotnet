using System;
namespace XiEditorCore
{
    public enum EditMethods
    {
        Insert,
        Paste,
        Copy,
        Cut,
        Scroll,
        Resize,
        Gesture,
        GotoLine,
    }

    public enum EditMovement
    {
        MoveUp,
        MoveUpAndModifySelection,
        MoveDown,
        MoveDownAndModifySelection,
        MoveLeft,
        MoveLeftAndModifySelection,
        MoveRight,
        MoveRightAndModifySelection,
        MoveWordLeft,
        MoveWordLeftAndModifySelection,
        MoveWordRight,
        MoveWordRightAndModifySelection,
        MoveToBeginningOfParagraph,
        MoveToBeginningOfParagraphAndModifySelection,
        MoveToEndOfParagraph,
        MoveToEndOfParagraphAndModifySelection,
        MoveToLeftEndOfLine,
        MoveToLeftEndOfLineAndModifySelection,
        MoveToRightEndOfLine,
        MoveToRightEndOfLineAndModifySelection,
        MoveToBeginningOfDocument,
        MoveToBeginningOfDocumentAndModifySelection,
        MoveToEndOfDocument,
        MoveToEndOfDocumentAndModifySelection,
    }

    public enum EditDelete
    {
        DeleteBackward,
        DeleteForward,
        DeleteWordForward,
        DeleteWordBackward,
        DeleteToEndOfParagraph,
        DeleteToBeginningOfLine,
    }

    public enum ParameterlessEditMethods
    {
        InsertNewline,
        InsertTab,
        DuplicateLine,


        ScrollPageUp,
        PageUpAndModifySelection,
        ScrollPageDown,
        PageDownAndModifySelection,
        Yank,
        Transpose,
        SelectAll,
        CollapseSelections,
        AddSelectionAbove,
        AddSelectionBelow,
        Undo,
        Redo,
    }

    #region Other
    public enum BackendTransformTextMethods
    {
        Uppercase,
        Lowercase,
        Capitalize,
        Indent,
        Outdent,
    }

    public enum BackendTransformSelection
    {
        IncreaseNumber,
        DecreaseNumber,
    }

    public enum BackendFindAndReplaceMethods
    {
        Find,
        MultiFind,
        FindNext,
        FindPrevious,
        FindAll,
        HighlightFind,
        SelectionForFind,
        Replace,
        SelectionForReplace,
        ReplaceNext,
        ReplaceAll,
        SelectionIntoLines,
    }
    #endregion

}
