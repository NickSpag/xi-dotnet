#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using SkiaSharp;
using SkiaSharp.Views.Mac;
using XiEditorCore.Models;

namespace XiEditor
{
    public class TextCache
    {
        public List<TextLine> OldLines = new List<TextLine>();

        public List<TextLine> NewLines = new List<TextLine>();

        public int LogicalLineNumber { get; set; } = 0;

        public int OldIndex { get; set; } = 0;

        public int LastLineNumber { get; set; }

        public void UpdateCache(List<Operation> operations)
        {
            OldLines = NewLines;
            NewLines = new List<TextLine>();
            OldIndex = 0;

            foreach (var operation in operations)
            {
                //"copy" | "skip" | "invalidate" | "update" | "ins"
                switch (operation.Identifier)
                {
                    case "skip":
                        Skip(operation);
                        break;
                    case "ins":
                        Insert(operation);
                        break;
                    case "copy":
                        Copy(operation);
                        break;
                    default:
                        System.Console.WriteLine($"Unaccounted for Update Operation: {operation.Identifier}");
                        break;
                }
            }

            if(NewLines.Last() is TextLine lastLine)
            {
                LastLineNumber = lastLine.Ln;
            }
        }

        private void Skip(Operation operation)
        {
            OldIndex += operation.N;
        }

        public void Copy(Operation copyOperation)
        {
            NewLines.AddRange(OldLines.GetRange(OldIndex, copyOperation.N));
            OldIndex += copyOperation.N;
        }

        public void Insert(Operation insertOperation)
        {
            NewLines.AddRange(insertOperation.TextLines);
        }
    }

    public class SimplifiedViewSkiaRenderer
    {
        const int yMargin = 10;
        const int xMargin = 10;

        //gutter todo_
        //const int leftTextPadding = 4;

        readonly TextCache cache;
        readonly SimplifiedView parentView;
        readonly SKPaint cachedCursorPaint;
        readonly SKPaint cachedTextPaint;
        readonly float cachedTextLineHeight;
        readonly float cachedTextCharacterWidth;

        UpdateNotification? latestUpdate;

        public SimplifiedViewSkiaRenderer(SimplifiedView parentView)
        {
            this.parentView = parentView;
            this.cache = new TextCache();

            cachedTextPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Left,

                TextSize = 18,
                Typeface = SKTypeface.FromFamilyName("Courier New"),
            };

            cachedCursorPaint = new SKPaint()
            {
                Color = SKColors.DarkGray,
                Style = SKPaintStyle.Fill,
                IsStroke = true,
            };

            cachedTextLineHeight = cachedTextPaint.FontSpacing;
            cachedTextCharacterWidth = cachedTextPaint.MeasureText("_");
        }

        public void StoreLatestUpdate(UpdateNotification update)
        {
            latestUpdate = update;
        }

        public void OnPaintGLSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            // wipe canvas
            var canvas = e.Surface.Canvas;
            canvas.Scale((float)parentView.Window.BackingScaleFactor);
            canvas.Clear(SKColors.White);

            if(latestUpdate?.Params?.Update?.Operations is List<Operation> operations)
            {
                cache.UpdateCache(operations);

                foreach (var line in cache.NewLines)
                {
                    if(line.Cursor is List<int> cursorPositions)
                    {
                        if(cursorPositions.Count > 1)
                        {
                            System.Console.WriteLine("FOUND EXTRA CURSOR");
                        }

                        PaintCursor(canvas, line.Ln, cursorPositions.First());
                    }

                    PaintTextLine(canvas, line);
                }
            }
            else
            {
                PaintCursor(canvas, line: 1, 0);
            }
        }

        //private void PaintLineGutter(SKCanvas canvas)
        //{
        //    var lastLineNumber = cache.LastLineNumber.ToString();
        //    var width = lastLineNumber.Length * cachedTextCharacterWidth;

        //    foreach (var line in cache.NewLines)
        //    {

        //    }

        //    var maxLineDigit = cache.LastLineNumber;

        //}

        //we draw cursors within a line, and after a column of text, so lines start at 1, columns start at 0
        private void PaintCursor(SKCanvas canvas, int line, int column)
        {

            var x = (column * cachedTextCharacterWidth) + xMargin;

            var rawLineHeight = (line) * cachedTextLineHeight;

            var cursorBottomDistanceFromFrameTop = rawLineHeight + yMargin;
            // we draw the cursor for the full line, but could be room for finessing here. 
            var cursorTopDistanceFromFrameTop = cursorBottomDistanceFromFrameTop - cachedTextLineHeight;

            var cursorBottomPoint = new SKPoint(x, cursorTopDistanceFromFrameTop);
            var cursorTopPoint = new SKPoint(x, cursorBottomDistanceFromFrameTop);

            canvas.DrawLine(cursorBottomPoint, cursorTopPoint, cachedCursorPaint);
        }

        private void PaintTextLine(SKCanvas canvas, XiEditorCore.Models.TextLine line)
        {
            //line's start at 1, so this is inclusive
            var rawLineHeight = line.Ln * cachedTextLineHeight;

            // we subtract descent from the raw line height, since draw text is done from the baseline we want to offset it
            var drawOrigin = new SKPoint(xMargin, yMargin + rawLineHeight - cachedTextPaint.FontMetrics.Descent);

            canvas.DrawText(line.Text, drawOrigin, cachedTextPaint);
        }
    }
}
