using System.Text.Json.Serialization;
using XiEditorCore.Enums;

namespace XiEditorCore.Models
{
    public class EditNotification
    {
        [JsonPropertyName("method")]
        public string Method => "edit";

        [JsonPropertyName("params")]
        public EditParameters Parameters { get; set; }

        public EditNotification(string viewId, char @char)
        {
            Parameters = new EditParameters(viewId, @char);
        }
    }

    public class EditInnerMethodParameterless
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
    }

    public class EditWithoutInnerParameters : IRPCNotification
    {
        [JsonIgnore]
        public string Method => Constants.XiMethods.Edit;

        [JsonPropertyName("method")]
        public string SubMethod { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }

        public EditWithoutInnerParameters(string viewId, EditSubMethodWithoutParameters subMethod)
        {
            ViewId = viewId;
            SubMethod = XiMethodKeyForSubmethod(subMethod);
        }

        private static string XiMethodKeyForSubmethod(EditSubMethodWithoutParameters subMethod) => subMethod switch
        {
            EditSubMethodWithoutParameters.MoveUp => Constants.XiMethods.EditSubMethods.MoveUp,
            EditSubMethodWithoutParameters.MoveUpAndModifySelection => Constants.XiMethods.EditSubMethods.MoveUpAndModifySelection,
            EditSubMethodWithoutParameters.MoveDown => Constants.XiMethods.EditSubMethods.MoveDown,
            EditSubMethodWithoutParameters.MoveDownAndModifySelection => Constants.XiMethods.EditSubMethods.MoveDownAndModifySelection,
            EditSubMethodWithoutParameters.MoveLeft => Constants.XiMethods.EditSubMethods.MoveLeft,
            EditSubMethodWithoutParameters.MoveLeftAndModifySelection => Constants.XiMethods.EditSubMethods.MoveLeftAndModifySelection,
            EditSubMethodWithoutParameters.MoveRight => Constants.XiMethods.EditSubMethods.MoveRight,
            EditSubMethodWithoutParameters.MoveRightAndModifySelection => Constants.XiMethods.EditSubMethods.MoveRightAndModifySelection,
            EditSubMethodWithoutParameters.MoveWordLeft => Constants.XiMethods.EditSubMethods.MoveWordLeft,
            EditSubMethodWithoutParameters.MoveWordLeftAndModifySelection => Constants.XiMethods.EditSubMethods.MoveWordLeftAndModifySelection,
            EditSubMethodWithoutParameters.MoveWordRight => Constants.XiMethods.EditSubMethods.MoveWordRight,
            EditSubMethodWithoutParameters.MoveWordRightAndModifySelection => Constants.XiMethods.EditSubMethods.MoveWordRightAndModifySelection,
            EditSubMethodWithoutParameters.MoveToBeginningOfParagraph => Constants.XiMethods.EditSubMethods.MoveToBeginningOfParagraph,
            EditSubMethodWithoutParameters.MoveToBeginningOfParagraphAndModifySelection => Constants.XiMethods.EditSubMethods.MoveToBeginningOfParagraphAndModifySelection,
            EditSubMethodWithoutParameters.MoveToEndOfParagraph => Constants.XiMethods.EditSubMethods.MoveToEndOfParagraph,
            EditSubMethodWithoutParameters.MoveToEndOfParagraphAndModifySelection => Constants.XiMethods.EditSubMethods.MoveToEndOfParagraphAndModifySelection,
            EditSubMethodWithoutParameters.MoveToLeftEndOfLine => Constants.XiMethods.EditSubMethods.MoveToLeftEndOfLine,
            EditSubMethodWithoutParameters.MoveToLeftEndOfLineAndModifySelection => Constants.XiMethods.EditSubMethods.MoveToLeftEndOfLineAndModifySelection,
            EditSubMethodWithoutParameters.MoveToRightEndOfLine => Constants.XiMethods.EditSubMethods.MoveToRightEndOfLine,
            EditSubMethodWithoutParameters.MoveToRightEndOfLineAndModifySelection => Constants.XiMethods.EditSubMethods.MoveToRightEndOfLineAndModifySelection,
            EditSubMethodWithoutParameters.MoveToBeginningOfDocument => Constants.XiMethods.EditSubMethods.MoveToBeginningOfDocument,
            EditSubMethodWithoutParameters.MoveToBeginningOfDocumentAndModifySelection => Constants.XiMethods.EditSubMethods.MoveToBeginningOfDocumentAndModifySelection,
            EditSubMethodWithoutParameters.MoveToEndOfDocument => Constants.XiMethods.EditSubMethods.MoveToEndOfDocument,
            EditSubMethodWithoutParameters.MoveToEndOfDocumentAndModifySelection => Constants.XiMethods.EditSubMethods.MoveToEndOfDocumentAndModifySelection,
            EditSubMethodWithoutParameters.DeleteBackward => Constants.XiMethods.EditSubMethods.DeleteBackward,
            EditSubMethodWithoutParameters.DeleteForward => Constants.XiMethods.EditSubMethods.DeleteForward,
            EditSubMethodWithoutParameters.DeleteWordForward => Constants.XiMethods.EditSubMethods.DeleteWordForward,
            EditSubMethodWithoutParameters.DeleteWordBackward => Constants.XiMethods.EditSubMethods.DeleteWordBackward,
            EditSubMethodWithoutParameters.DeleteToEndOfParagraph => Constants.XiMethods.EditSubMethods.DeleteToEndOfParagraph,
            EditSubMethodWithoutParameters.DeleteToBeginningOfLine => Constants.XiMethods.EditSubMethods.DeleteToBeginningOfLine,
            EditSubMethodWithoutParameters.InsertNewline => Constants.XiMethods.EditSubMethods.InsertNewline,
            EditSubMethodWithoutParameters.InsertTab => Constants.XiMethods.EditSubMethods.InsertTab,
            EditSubMethodWithoutParameters.DuplicateLine => Constants.XiMethods.EditSubMethods.DuplicateLine,
            EditSubMethodWithoutParameters.ScrollPageUp => Constants.XiMethods.EditSubMethods.ScrollPageUp,
            EditSubMethodWithoutParameters.PageUpAndModifySelection => Constants.XiMethods.EditSubMethods.PageUpAndModifySelection,
            EditSubMethodWithoutParameters.ScrollPageDown => Constants.XiMethods.EditSubMethods.ScrollPageDown,
            EditSubMethodWithoutParameters.PageDownAndModifySelection => Constants.XiMethods.EditSubMethods.PageDownAndModifySelection,
            EditSubMethodWithoutParameters.Yank => Constants.XiMethods.EditSubMethods.Yank,
            EditSubMethodWithoutParameters.Transpose => Constants.XiMethods.EditSubMethods.Transpose,
            EditSubMethodWithoutParameters.SelectAll => Constants.XiMethods.EditSubMethods.SelectAll,
            EditSubMethodWithoutParameters.CollapseSelections => Constants.XiMethods.EditSubMethods.CollapseSelections,
            EditSubMethodWithoutParameters.AddSelectionAbove => Constants.XiMethods.EditSubMethods.AddSelectionAbove,
            EditSubMethodWithoutParameters.AddSelectionBelow => Constants.XiMethods.EditSubMethods.AddSelectionBelow,
            EditSubMethodWithoutParameters.Undo => Constants.XiMethods.EditSubMethods.Undo,
            EditSubMethodWithoutParameters.Redo => Constants.XiMethods.EditSubMethods.Redo,
            EditSubMethodWithoutParameters.Uppercase => Constants.XiMethods.EditSubMethods.Uppercase,
            EditSubMethodWithoutParameters.Lowercase => Constants.XiMethods.EditSubMethods.Lowercase,
            EditSubMethodWithoutParameters.Capitalize => Constants.XiMethods.EditSubMethods.Capitalize,
            EditSubMethodWithoutParameters.Indent => Constants.XiMethods.EditSubMethods.Indent,
            EditSubMethodWithoutParameters.Outdent => Constants.XiMethods.EditSubMethods.Outdent,
            EditSubMethodWithoutParameters.IncreaseNumber => Constants.XiMethods.EditSubMethods.IncreaseNumber,
            EditSubMethodWithoutParameters.DecreaseNumber => Constants.XiMethods.EditSubMethods.DecreaseNumber,
        };
    }

    public class EditWithInnerParameters : IRPCNotification
    {
        [JsonIgnore]
        public string Method => Constants.XiMethods.Edit;

        [JsonPropertyName("method")]
        public string SubMethod { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }

        //todo the inner param objects
        public EditWithInnerParameters(string viewId, EditSubMethodWithParameters innerMethod)
        {
            ViewId = viewId;
            SubMethod = XiMethodForInnerEnum(innerMethod);
        }

        private static string XiMethodForInnerEnum(EditSubMethodWithParameters inner) => inner switch
        {
            EditSubMethodWithParameters.Insert => Constants.XiMethods.EditSubMethods.Insert,
            EditSubMethodWithParameters.Paste => Constants.XiMethods.EditSubMethods.Paste,
            EditSubMethodWithParameters.Copy => Constants.XiMethods.EditSubMethods.Copy,
            EditSubMethodWithParameters.Cut => Constants.XiMethods.EditSubMethods.Cut,
            EditSubMethodWithParameters.Scroll => Constants.XiMethods.EditSubMethods.Scroll,
            EditSubMethodWithParameters.Resize => Constants.XiMethods.EditSubMethods.Resize,
            EditSubMethodWithParameters.Gesture => Constants.XiMethods.EditSubMethods.Gesture,
            EditSubMethodWithParameters.GotoLine => Constants.XiMethods.EditSubMethods.GotoLine,
        };
    }

    public class EditParameters
    {
        [JsonPropertyName("method")]
        public string Method => "insert";

        [JsonPropertyName("params")]
        public InsertParameters Parameters { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }

        public EditParameters(string viewId, char @char)
        {
            ViewId = viewId;
            Parameters = new InsertParameters(@char);
        }
    }

    public class InsertParameters
    {
        [JsonPropertyName("chars")]
        public char Chars { get; set; }

        public InsertParameters(char @char)
        {
            Chars = @char;
        }
    }
}
