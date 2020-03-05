#nullable enable

using System;
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

        public SimplifiedViewSkiaRenderer(SimplifiedView parentView)
        {
            this.parentView = parentView;
        }

        public void StoreLatestUpdate(UpdateNotification update)
        {
            latestUpdate = update;
        }

        public void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (!(latestUpdate?.Params.Update.Operations.First()
                .Lines.First()
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
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };
            var coord = new SKPoint((float)parentView.Bounds.Width / 2, ((float)parentView.Bounds.Height + paint.TextSize) / 2);

            canvas.DrawText(text, coord, paint);
        }
    }
}
