using System.Text.Json.Serialization;
using XiEditorCore.Enums;

namespace XiEditorCore.Models
{

    public class EditWithoutSubmethodParameters : IRPCNotification
    {
        [JsonIgnore]
        public string Method => Constants.XiMethods.Edit;

        [JsonPropertyName("method")]
        public string SubMethod { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }

        public EditWithoutSubmethodParameters(string viewId, EditSubMethodWithoutParameters subMethod)
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
            //not in protocol docs, but in xi-mac
            EditSubMethodWithoutParameters.MoveForward => Constants.XiMethods.EditSubMethods.MoveForward,
            EditSubMethodWithoutParameters.MoveBackward => Constants.XiMethods.EditSubMethods.MoveBackward,
        };
    }

    public class EditWithSubmethodParameters<T> : IRPCNotification
        where T: IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Method => Constants.XiMethods.Edit;

        [JsonPropertyName("method")]
        public string Submethod { get; set; }

        [JsonPropertyName("view_id")]
        public string ViewId { get; set; }

        [JsonPropertyName("params")]
        public T SubmethodParameters { get; set; }

        public EditWithSubmethodParameters(string viewId, T submethodParameters)
        {
            ViewId = viewId;
            Submethod = submethodParameters.Submethod;
            SubmethodParameters = submethodParameters;
        }

    }

    public class InsertParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Insert;

        [JsonPropertyName("chars")]
        public string Text { get; set; }

        public InsertParameters(string text)
        {
            Text = text;
        }
    }

    public class PasteParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Paste;

        //TODO Params
    }

    // warning- request style here
    public class CopyParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Copy;

        //TODO Params
    }

    public class CutParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Cut;

        //TODO Params
    }

    public class ScrollParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Scroll;

        //TODO Params
    }

    public class ResizeParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Resize;

        //TODO Params
    }

    public class GestureParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.Gesture;

        //TODO Params
    }

    public class GotoLineParameters : IEditSubmethodParameters
    {
        [JsonIgnore]
        public string Submethod => Constants.XiMethods.EditSubMethods.GotoLine;

        //TODO Params
    }
}
