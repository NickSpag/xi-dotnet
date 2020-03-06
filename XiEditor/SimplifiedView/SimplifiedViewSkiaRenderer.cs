#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using SkiaSharp;
using SkiaSharp.Views.Mac;
using XiEditorCore.Models;

namespace XiEditor
{
    public class LineCache
    {
        public List<XiEditorCore.Models.TextLine> CachedLines = new global::System.Collections.Generic.List<global::XiEditorCore.Models.TextLine>();

        public List<XiEditorCore.Models.TextLine> GetLinesFromLast(int count)
        {
            var index = CachedLines.Count - count - 1;
            return CachedLines.GetRange(index, count);
        }

        public XiEditorCore.Models.TextLine GetLineAt(int lineNumber) => CachedLines[lineNumber];
        public void AddLine(XiEditorCore.Models.TextLine line)
        {
            if(line.Ln > CachedLines.Count)
            {
                CachedLines.Add(line);
            }
            else
            {
                CachedLines[line.Ln - 1] = line;
            }
        }
    }

    public class SimplifiedViewSkiaRenderer
    {
        const int yMargin = 10;
        const int xMargin = 10;

        readonly SimplifiedView parentView;
        readonly LineCache lineCache;
        readonly SKPaint cachedCursorPaint;
        readonly SKPaint cachedTextPaint;
        readonly float cachedTextLineHeight;
        readonly float cachedTextCharacterWidth;

        // since we want a cursor to show even without text and use the line number for height
        // we pretend there is always a line 1, but a column 0 is possible
        int line = 0;
        int column = 0;

        UpdateNotification? latestUpdate;

        public SimplifiedViewSkiaRenderer(SimplifiedView parentView)
        {
            this.parentView = parentView;
            this.lineCache = new LineCache();

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

        public void StoreScrollPosition(ScrollToNotification scrollTo)
        {
            line = scrollTo.Params.Line;
            column = scrollTo.Params.Column;
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

            if (latestUpdate?.Params?.Update is Update update && update.Operations.Any())
            {
                foreach (var operation in update.Operations)
                {
                    switch (operation.Identifier)
                    {
                        case "ins":
                            foreach (var line in operation.TextLines)
                            {
                                lineCache.AddLine(line);
                                PaintTextLine(canvas, line);
                            }
                            break;
                            //todo theres no deletion of the cache when we remove a line, so copy's operation.N is requesting to copy N back from the "current line"
                        case "copy":
                            var lines = lineCache.GetLinesFromLast(operation.N);
                            foreach (var cachedLine in lines)
                            {
                                PaintTextLine(canvas, cachedLine);
                            }
                            break;
                        default:
                            System.Console.WriteLine($"UNRECOGNIZED OPERATION:" + operation);
                            break;
                    }
                }
            }

            PaintCursor(canvas, line, column);
        }

        private void PaintCursor(SKCanvas canvas, int line, int column)
        {

            var x = (column * cachedTextCharacterWidth) + xMargin;

            //we're drawing from the top. lines start at 0
            var rawLineHeight = (line + 1) * cachedTextLineHeight;

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
