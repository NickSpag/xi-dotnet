#nullable enable

using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Mac;
using XiEditorCore.Models;

namespace XiEditor
{
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
