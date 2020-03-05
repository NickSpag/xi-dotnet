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
            if(MapCommandToEditSubmethod(command, out EditSubMethodWithoutParameters subMethod))
            {
                XiView?.PerformCommand(subMethod);
            }
            else
            {
                System.Console.WriteLine($"UNACCOUNTED FOR COMMAND: {command}");
            }
        }

        /// <summary>
        /// Marked means "selected." Like with the cursor.
        /// </summary>
        /// <param name="text">Text getting inserted</param>
        /// <param name="replacementRange">This is the selected text that is being replaced with the insertion.
        /// In the event that a user highlighted text and then inserted, we expect that text to be deleted as well.</param>
        #region NSTextInputClient
        [Export("insertText:replacementRange:")]
        public void InsertText(NSObject text, NSRange replacementRange)
        {
            //todo selection
            //RemoveMarkedText();

            // if a replacement range exists, send a delete command for each character in the range. We don't need to specify a position or anything
            // since having been selected necesssitates that the cursor is at the appropriate position to delete
            if(replacementRange.Location != NSRange.NotFound)
            {
                XiView?.Delete((int)replacementRange.Length);
            }

            switch (text)
            {
                case NSAttributedString attrString:
                    XiView?.Insert(attrString.ToString());
                    break;
                case NSString nsString:
                    XiView?.Insert(nsString.ToString());
                    break;
                default:
                    break;
            }

            // this is in xi-mac, that method that does two things, from two places. not sure its very clean
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

        private bool MapCommandToEditSubmethod(string command, out EditSubMethodWithoutParameters submethod)
        {
            submethod = command switch
            {
                "deleteBackward:" => EditSubMethodWithoutParameters.DeleteBackward,
                "deleteForward:" => EditSubMethodWithoutParameters.DeleteForward,
                "deleteToBeginningOfLine:" => EditSubMethodWithoutParameters.DeleteToBeginningOfLine,
                "deleteToEndOfParagraph:" => EditSubMethodWithoutParameters.DeleteToEndOfParagraph,
                "deleteWordBackward:" => EditSubMethodWithoutParameters.DeleteWordBackward,
                "deleteWordForward:" => EditSubMethodWithoutParameters.DeleteWordForward,
                "insertNewline:" => EditSubMethodWithoutParameters.InsertNewline,
                "insertTab:" => EditSubMethodWithoutParameters.InsertTab,
                "moveBackward:" => EditSubMethodWithoutParameters.MoveBackward,
                "moveDown:" => EditSubMethodWithoutParameters.MoveDown,
                "moveDownAndModifySelection:" => EditSubMethodWithoutParameters.MoveDownAndModifySelection,
                "moveForward:" => EditSubMethodWithoutParameters.MoveForward,
                "moveLeft:" => EditSubMethodWithoutParameters.MoveLeft,
                "moveLeftAndModifySelection:" => EditSubMethodWithoutParameters.MoveLeftAndModifySelection,
                "moveRight:" => EditSubMethodWithoutParameters.MoveRight,
                "moveRightAndModifySelection:" => EditSubMethodWithoutParameters.MoveRightAndModifySelection,
                "moveToBeginningOfDocument:" => EditSubMethodWithoutParameters.MoveToBeginningOfDocument,
                "moveToBeginningOfDocumentAndModifySelection:" => EditSubMethodWithoutParameters.MoveToBeginningOfDocumentAndModifySelection,
                "moveToBeginningOfLine:" => EditSubMethodWithoutParameters.MoveToLeftEndOfLine,
                "moveToBeginningOfLineAndModifySelection:" => EditSubMethodWithoutParameters.MoveToLeftEndOfLineAndModifySelection,
                "moveToBeginningOfParagraph:" => EditSubMethodWithoutParameters.MoveToBeginningOfParagraph,
                "moveToBeginningOfParagraphAndModifySelection:" => EditSubMethodWithoutParameters.MoveToBeginningOfParagraphAndModifySelection,
                "moveToEndOfDocument:" => EditSubMethodWithoutParameters.MoveToEndOfDocument,
                "moveToEndOfDocumentAndModifySelection:" => EditSubMethodWithoutParameters.MoveToEndOfDocumentAndModifySelection,
                "moveToEndOfLine:" => EditSubMethodWithoutParameters.MoveToRightEndOfLine,
                "moveToEndOfLineAndModifySelection:" => EditSubMethodWithoutParameters.MoveToRightEndOfLineAndModifySelection,
                "moveToEndOfParagraph:" => EditSubMethodWithoutParameters.MoveToEndOfParagraph,
                "moveToEndOfParagraphAndModifySelection:" => EditSubMethodWithoutParameters.MoveToEndOfParagraphAndModifySelection,
                "moveToLeftEndOfLine:" => EditSubMethodWithoutParameters.MoveToLeftEndOfLine,
                "moveToLeftEndOfLineAndModifySelection:" => EditSubMethodWithoutParameters.MoveToLeftEndOfLineAndModifySelection,
                "moveToRightEndOfLine:" => EditSubMethodWithoutParameters.MoveToRightEndOfLine,
                "moveToRightEndOfLineAndModifySelection:" => EditSubMethodWithoutParameters.MoveToRightEndOfLineAndModifySelection,
                "moveUp:" => EditSubMethodWithoutParameters.MoveUp,
                "moveUpAndModifySelection:" => EditSubMethodWithoutParameters.MoveUpAndModifySelection,
                "moveWordLeft:" => EditSubMethodWithoutParameters.MoveWordLeft,
                "moveWordLeftAndModifySelection:" => EditSubMethodWithoutParameters.MoveWordLeftAndModifySelection,
                "moveWordRight:" => EditSubMethodWithoutParameters.MoveWordRight,
                "moveWordRightAndModifySelection:" => EditSubMethodWithoutParameters.MoveWordRightAndModifySelection,
                "pageDownAndModifySelection:" => EditSubMethodWithoutParameters.PageDownAndModifySelection,
                "pageUpAndModifySelection:" => EditSubMethodWithoutParameters.PageUpAndModifySelection,
                "scrollPageDown:" => EditSubMethodWithoutParameters.ScrollPageDown,
                "scrollPageUp:" => EditSubMethodWithoutParameters.ScrollPageUp,
                "scrollToBeginningOfDocument:" => EditSubMethodWithoutParameters.MoveToBeginningOfDocument,
                "scrollToEndOfDocument:" => EditSubMethodWithoutParameters.MoveToEndOfDocument,
                "transpose:" => EditSubMethodWithoutParameters.Transpose,
                "yank:" => EditSubMethodWithoutParameters.Yank,
            };

            // if it was assigned, return true;
            return (submethod != 0);
        }
    }
}
