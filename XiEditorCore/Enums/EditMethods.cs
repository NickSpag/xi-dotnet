using System;

namespace XiEditorCore.Enums
{
    public enum EditSubMethodWithParameters
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

    public enum EditSubMethodWithoutParameters
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
    // edit delete
        DeleteBackward,
        DeleteForward,
        DeleteWordForward,
        DeleteWordBackward,
        DeleteToEndOfParagraph,
        DeleteToBeginningOfLine,
    // edit other
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
    // enum EditTransformText
        Uppercase,
        Lowercase,
        Capitalize,
        Indent,
        Outdent,
    // enum EditTransformSelection
        IncreaseNumber,
        DecreaseNumber,
    }
}
