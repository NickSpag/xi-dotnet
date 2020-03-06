#nullable enable

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
        readonly SimplifiedView parentView;

        UpdateNotification? latestUpdate;

        SKPaint cachedPaint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Left,

            TextSize = 24,
            Typeface = SKTypeface.FromFamilyName("Courier New"),
          };

        public SimplifiedViewSkiaRenderer(SimplifiedView parentView)
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
            if (!(latestUpdate?.Params?.Update?.Operations?.First()
                .Lines?.First()
                .Text is string firstLineOfText))
            {
                return;
            }

            PaintSurface(firstLineOfText, e);
        }

        void PaintSurface(string text, SKPaintSurfaceEventArgs args)
        {
            // the the canvas and properties
            var canvas = args.Surface.Canvas;

            // get the screen density for scaling
            var scale = (float)parentView.Window.BackingScaleFactor;

            // handle the device screen density
            canvas.Scale(scale);

            // make sure the canvas is blank
            canvas.Clear(SKColors.White);

            // draw some text
            var coord = new SKPoint((float)parentView.Bounds.Width / 2, ((float)parentView.Bounds.Height + cachedPaint.TextSize) / 2);

            canvas.DrawText(text, coord, cachedPaint);
        }
    }
}
