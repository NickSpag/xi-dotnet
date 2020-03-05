#nullable enable

using System;

using Foundation;
using AppKit;
using CoreGraphics;
using SkiaSharp.Views.Mac;

using XiEditorCore;
using XiEditorCore.Enums;

namespace XiEditor
{
	public partial class SimplifiedView : SKCanvasView, INSTextInputClient
    {
        readonly SimplifiedViewSkiaRenderer renderer;

        XiViewHandle? _xiView;

		NSRange _selectedRange = new NSRange(NSRange.NotFound, 0);
		NSRange _markedRange = new NSRange(NSRange.NotFound, 0);

        public XiViewHandle? XiView
        {
            get => _xiView;
            set
            {
                if (value is null) return;

                _xiView = value;
                _xiView.NeedsUpdate += _xiView_NeedsUpdate;
                _xiView.NeedsScrollTo += _xiView_NeedsScrollTo;
                _xiView.NeedsMeasurement += _xiView_NeedsMeasurement;
            }
        }

        private void _xiView_NeedsMeasurement(object sender, MeasureWidthEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _xiView_NeedsScrollTo(object sender, ScrollToEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _xiView_NeedsUpdate(object sender, UpdateEventArgs e)
        {
            renderer.StoreLatestUpdate(e.Notification);

            this.InvokeOnMainThread(() => this.NeedsDisplay = true);
        }

        public SimplifiedView (IntPtr handle) : base (handle)
		{
            renderer = new SimplifiedViewSkiaRenderer(this);

            //SKCanvasView's core drawing method
            PaintSurface += renderer.OnPaintSurface;
        }

        public override bool AcceptsFirstResponder() => true;

        public void HandleCommand(string command)
        {
            if (XiEditMethodFromAppkitCommand(command, out string method))
            {
                //todo change the above method to the enum
                //create a method on Xiview that takes enum
                //xiview calls xicore with json or whatever
            }

            if (!string.IsNullOrEmpty(method))
            {
                System.Console.WriteLine(method);
            }
        }

        /// <summary>
        /// Marked means "selected." Like with the cursor.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replacementRange"></param>
        #region NSTextInputClient
        [Export("insertText:replacementRange:")]
        public void InsertText(NSObject text, NSRange replacementRange)
        {
            //RemoveMarkedText();
            //_ = ReplaceCharactersInRange(replacementRange, text);
        }

        [Export("characterIndexForPoint:")]
        // returns 0 in the orignal. not sure what this method is for on the protocol
        public nuint GetCharacterIndex(CGPoint point) => 0;

        [Export("setMarkedText:selectedRange:replacementRange:")]
        public void SetMarkedText(NSObject text, NSRange selectedRange, NSRange replacementRange)
        {
            //todo
            throw new System.NotImplementedException();
        }

        [Export("unmarkText")]
        public void UnmarkText()
        {
            //todo
            throw new System.NotImplementedException();
        }

        [Export("selectedRange")]
        public NSRange SelectedRange => _selectedRange;

        [Export("markedRange")]
        public NSRange MarkedRange => _markedRange;

        [Export("hasMarkedText")]
        public bool HasMarkedText => _markedRange.Location != NSRange.NotFound;

        [Export("validAttributesForMarkedText")]
        public NSString[] ValidAttributesForMarkedText =>
            new NSString[] { NSStringAttributeKey.ForegroundColor, NSStringAttributeKey.BackgroundColor };

        [Export("attributedSubstringForProposedRange:actualRange:")]
        public NSAttributedString GetAttributedSubstring(NSRange proposedRange, out NSRange actualRange)
        {
            //todo
            throw new System.NotImplementedException();
        }

        [Export("firstRectForCharacterRange:actualRange:")]
        public CGRect GetFirstRect(NSRange characterRange, out NSRange actualRange)
        {
            //todo
            throw new System.NotImplementedException();
        }

        /// non-protocol, but required for protocol implementation

        public void RemoveMarkedText()
        {
            //todo
            throw new System.NotImplementedException();
        }

        public NSRange ReplacementMarkedRange(NSRange replacementRange)
        {
            //todo
            throw new System.NotImplementedException();
        }

        public NSRange ReplaceCharactersInRange(NSRange replacementRange, NSObject text)
        {
            //todo
            throw new System.NotImplementedException();
        }

        #endregion

        private bool XiEditMethodFromAppkitCommand(string command, out string methodName)
        {
            methodName = command switch
            {
                "deleteBackward:" => "delete_backward",
                "deleteForward:" => "delete_forward",
                "deleteToBeginningOfLine:" => "delete_to_beginning_of_line",
                "deleteToEndOfParagraph:" => "delete_to_end_of_paragraph",
                "deleteWordBackward:" => "delete_word_backward",
                "deleteWordForward:" => "delete_word_forward",
                "insertNewline:" => "insert_newline",
                "insertTab:" => "insert_tab",
                "moveBackward:" => "move_backward",
                "moveDown:" => "move_down",
                "moveDownAndModifySelection:" => "move_down_and_modify_selection",
                "moveForward:" => "move_forward",
                "moveLeft:" => "move_left",
                "moveLeftAndModifySelection:" => "move_left_and_modify_selection",
                "moveRight:" => "move_right",
                "moveRightAndModifySelection:" => "move_right_and_modify_selection",
                "moveToBeginningOfDocument:" => "move_to_beginning_of_document",
                "moveToBeginningOfDocumentAndModifySelection:" => "move_to_beginning_of_document_and_modify_selection",
                "moveToBeginningOfLine:" => "move_to_left_end_of_line",
                "moveToBeginningOfLineAndModifySelection:" => "move_to_left_end_of_line_and_modify_selection",
                "moveToBeginningOfParagraph:" => "move_to_beginning_of_paragraph",
                "moveToBeginningOfParagraphAndModifySelection:" => "move_to_beginning_of_paragraph_and_modify_selection",
                "moveToEndOfDocument:" => "move_to_end_of_document",
                "moveToEndOfDocumentAndModifySelection:" => "move_to_end_of_document_and_modify_selection",
                "moveToEndOfLine:" => "move_to_right_end_of_line",
                "moveToEndOfLineAndModifySelection:" => "move_to_right_end_of_line_and_modify_selection",
                "moveToEndOfParagraph:" => "move_to_end_of_paragraph",
                "moveToEndOfParagraphAndModifySelection:" => "move_to_end_of_paragraph_and_modify_selection",
                "moveToLeftEndOfLine:" => "move_to_left_end_of_line",
                "moveToLeftEndOfLineAndModifySelection:" => "move_to_left_end_of_line_and_modify_selection",
                "moveToRightEndOfLine:" => "move_to_right_end_of_line",
                "moveToRightEndOfLineAndModifySelection:" => "move_to_right_end_of_line_and_modify_selection",
                "moveUp:" => "move_up",
                "moveUpAndModifySelection:" => "move_up_and_modify_selection",
                "moveWordLeft:" => "move_word_left",
                "moveWordLeftAndModifySelection:" => "move_word_left_and_modify_selection",
                "moveWordRight:" => "move_word_right",
                "moveWordRightAndModifySelection:" => "move_word_right_and_modify_selection",
                "pageDownAndModifySelection:" => "page_down_and_modify_selection",
                "pageUpAndModifySelection:" => "page_up_and_modify_selection",
                "scrollPageDown:" => "scroll_page_down",
                "scrollPageUp:" => "scroll_page_up",
                // Not =>: these next two are mappings. Possible TODO to fix if core provides distinct behaviors
                "scrollToBeginningOfDocument:" => "move_to_beginning_of_document",
                "scrollToEndOfDocument:" => "move_to_end_of_document",
                "transpose:" => "transpose",
                "yank:" => "yank",
                _ => "",
            };

            // if we found a method, return true;
            return !string.IsNullOrEmpty(methodName);
        }
    }
}
