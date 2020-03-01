using System;
using CoreText;

using ArgbColor = System.UInt32;

namespace XiEditor
{
    public static class TextLineExtensions
    {
        public static (float, float, float, float) ArgbToFloat(ArgbColor color) =>
            (color >> 16 & 0xff,
             color >> 8 & 0xff,
             color & 0xff,
             color >> 24);

        public static void GetCTLineRange(CTLine line, SpanRange<int> range, out SpanRange<nfloat> lineRange)
        {
            var start = line.GetOffsetForStringIndex(range.Start);
            var end = line.GetOffsetForStringIndex(range.End);

            lineRange = new SpanRange<nfloat>(start: Min(ref start, ref end),
                                                   end: Max(ref start, ref end));
        }

        public static ref nfloat Min(ref nfloat x, ref nfloat y) => ref (x < y) ? ref x : ref y;
        public static ref nfloat Max(ref nfloat x, ref nfloat y) => ref (x > y) ? ref x : ref y;

    }
}
