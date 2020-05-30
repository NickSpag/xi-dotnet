using System;
using System.Collections.Generic;
using CoreText;
using Foundation;

using ArgbColor = System.UInt32;
using FontRef = System.UInt16;

namespace XiEditor
{
    public readonly struct SpanRange<T>
        where T : IComparable<T>
    {
        public readonly T Start;
        public readonly T End;

        public SpanRange(T start, T end)
        {
            Start = start;
            End = end;
        }

        public bool IsWithin(T value) => (value.CompareTo(Start) >= 0) &&
                                         (value.CompareTo(End) <= 0);
    }

    public struct ForegroundColorSpan
    {
        public ForegroundColorSpan(SpanRange<int> range, ArgbColor color)
        {
            Range = range;
            Color = color;
        }

        public readonly SpanRange<int> Range;
        public readonly ArgbColor Color;
    }

    public struct BackgroundColorSpan
    {
        public BackgroundColorSpan(SpanRange<int> range, ArgbColor color)
        {
            Range = range;
            Color = color;
        }

        public readonly SpanRange<int> Range;
        public readonly ArgbColor Color;
    }

    public struct UnderlineSpan
    {
        public UnderlineSpan(SpanRange<int> range, UnderlineStyle style)
        {
            Range = range;
            Style = style;
        }

        public readonly SpanRange<int> Range;
        public readonly UnderlineStyle Style;
    }

    // Represents a region of a `TextLine` that has a background color.
    // basically this represents the span generated from the ctline's actual position of the coloring
    public struct BackgroundColorPositioningSpan
    {
        public BackgroundColorPositioningSpan(SpanRange<nfloat> range, ArgbColor color)
        {
            Range = range;
            Color = color;
        }

        public readonly SpanRange<nfloat> Range;
        public readonly ArgbColor Color;
    }

    public struct UnderlinePositioningSpan
    {
        public UnderlinePositioningSpan(SpanRange<nfloat> xRange, SpanRange<nfloat> yRange, UnderlineStyle style)
        {
            XRange = xRange;
            YRange = yRange;
            Style = style;
        }

        public readonly SpanRange<nfloat> YRange;
        public readonly SpanRange<nfloat> XRange;
        public readonly UnderlineStyle Style;
    }

    #region Visual
    public struct GlyphInstance
    {
        public readonly FontRef RefId;
        //in pixels
        public readonly nfloat X;
        public readonly nfloat Y;

        public readonly (float, float, float, float) Color;

        public readonly UInt32 Flags;

        public GlyphInstance(FontRef fontRef, nfloat x, nfloat y, (float,float,float,float) color, UInt32 flags)
        {
            RefId = fontRef;
            X = x;
            Y = y;
            Color = color;
            Flags = flags;
        }
    }

    public enum UnderlineStyle
    {
        Single,
        Thick
    }
    #endregion

    public struct TextLineModel
    {
        public readonly List<GlyphInstance> Glyphs;
        public readonly CTLine Line;
        public readonly List<BackgroundColorPositioningSpan> BackgroundColorRanges;
        public readonly List<UnderlinePositioningSpan> UnderlineRanges;
        public readonly double Width;

        public TextLineModel(List<GlyphInstance> glyphs, CTLine line, List<BackgroundColorPositioningSpan> bgRanges, List<UnderlinePositioningSpan> underlineRanges, double? width)
        {
            Glyphs = glyphs;
            Line = line;
            BackgroundColorRanges = bgRanges;
            UnderlineRanges = underlineRanges;
            Width = width ?? line.GetTypographicBounds();
        }

        public nfloat GetOffsetForIndex(nint utf16Ix) => Line.GetOffsetForStringIndex(utf16Ix);

        //todo sig    func scatterGather<C: Collection>(indices glyphIndices: C, font: CTFont)

    }

    public class TextLineBuilder
    {
        const UInt32 FLAG_FAKE_ITALIC = (1 << 16);
        const UInt32 FLAG_HIGH_DPI = (1 << 17);

        readonly NSMutableAttributedString _attributedString;
        //this font is used for underline metrics. because it doesnt really have multi-font functionality, you probably dont actually need to
        //check the font for each glyph for underlining, like it does in the foreground glyphs? or underlining doesnt change? not sure
        //probably would need to restructre how the build method works to efficently get the font for underlining, if we do RTF

        readonly CTFont _font;

        readonly List<ForegroundColorSpan> _foregroundSpans = new List<ForegroundColorSpan>();
        readonly List<BackgroundColorSpan> _backgroundSpans = new List<BackgroundColorSpan>();

        //todo this whole structue should be "flag" Span even though right now its purely an italics thing
        readonly List<SpanRange<int>> _italicSpans = new List<SpanRange<int>>();
        readonly List<UnderlineSpan> _underlineRanges = new List<UnderlineSpan>();

        ArgbColor _defaultFontColor = 0xff000000;

        public TextLineBuilder(string text, CTFont font)
        {
            var attributes = new CTStringAttributes();
            attributes.Font = font;

            _attributedString = new NSMutableAttributedString(text, attributes);
            _font = font;
            
        }

        /// <summary>
        /// Sets the default color of the text with no syntax highlighting.
        /// </summary>
        /// <param name="color"></param>
        public void SetForegroundColor(ArgbColor color) => _defaultFontColor = color;

        /// Add a foreground color span to the text line. This method assumes that such spans
        /// are added in sorted order and do not overlap.
        public void AddForegroundSpan(SpanRange<int> range, ArgbColor color) => _foregroundSpans.Add(new ForegroundColorSpan(range, color));

        public void AddBackgroundSpan(SpanRange<int> range, ArgbColor color) => _backgroundSpans.Add(new BackgroundColorSpan(range, color));

        public void AddFakeItalicSpan(SpanRange<int> range) => _italicSpans.Add(range);

        public void AddUnderlineSpan(SpanRange<int> range, UnderlineStyle style) => _underlineRanges.Add(new UnderlineSpan(range, style));

        public TextLineModel Build(FontCache fontCache)
        {
            var line = new CTLine(_attributedString);

            var glyphs = BuildForegroundGlyphs(line, fontCache);
            var backgroundSpans = BuildBackgroundPositioningSpans(line);
            var underlineSpans = BuildUnderlines(line);

            return new TextLineModel(glyphs, line, backgroundSpans, underlineSpans, null);
        }

        private List<GlyphInstance> BuildForegroundGlyphs(CTLine line, FontCache fontCache)
        {
            var glyphs = new List<GlyphInstance>();

            // a  singular container that holds a color that will be given to a glyph, it is changed back and forth for each glyph index
            var currentColorContainer = TextLineExtensions.ArgbToFloat(_defaultFontColor);
            var runs = line.GetGlyphRuns();

            var foregroundSpanIndex = 0;
            var fakeItalicSpanIndex = 0;

            foreach (var run in runs)
            {

                var count = run.GlyphCount;
                var attributes = run.GetAttributes();

                var glyphsInRun = run.GetGlyphs();
                var positions = run.GetPositions();
                var indicies = run.GetStringIndices();

                var font = attributes.Font;
                var fontRef = fontCache.GetFontRefId(font);

                // step through each glpyh
                for (int i = 0; i < count; i++)
                {
                    var glyph = glyphsInRun[i];
                    var position = positions[i];
                    var index = indicies[i];

                    AdjustcurrentColorForGlyphIndex(ref foregroundSpanIndex, ref currentColorContainer, index);

                    var italicFlag = ProcessItalicSpan(ref fakeItalicSpanIndex, index);

                    glyphs.Add(new GlyphInstance(fontRef, position.X, position.Y, currentColorContainer, italicFlag));
                }
            }

            return glyphs;

            UInt32 ProcessItalicSpan(ref int italicSpanIndex, nint glyphIndex)
            {
                var italicSpanCounts = _italicSpans.Count;
                var italicSpan = _italicSpans[(int)glyphIndex];

                if (italicSpanIndex < italicSpanCounts &&
                    glyphIndex >= _italicSpans[italicSpanIndex].End)
                {
                    italicSpanIndex++;
                }

                // if current glyph index is within a range designated as italic, return italic flag, or return 0
                if (italicSpanIndex < italicSpanCounts &&
                    italicSpan.IsWithin((int)glyphIndex))
                {
                    return FLAG_FAKE_ITALIC;
                }
                else return 0;
            }

            void AdjustcurrentColorForGlyphIndex(ref int foregroundIndex, ref (float, float, float, float) currentColor, nint glyphIndex)
            {
                var colorSpanCounts = _foregroundSpans.Count;
                var colorSpan = _foregroundSpans[foregroundIndex];

                if (foregroundIndex < colorSpanCounts &&
                    glyphIndex >= _foregroundSpans[foregroundIndex].Range.End)
                {
                    foregroundIndex++;
                }

                if (foregroundIndex < colorSpanCounts &&
                    // if our glyph's current index is within a span designating a foreground color beides default, assign that color
                    colorSpan.Range.IsWithin((int)glyphIndex))
                {
                    currentColor = TextLineExtensions.ArgbToFloat(colorSpan.Color);
                }
                else
                {
                    currentColor = TextLineExtensions.ArgbToFloat(_defaultFontColor);
                }
            }
        }

        private List<BackgroundColorPositioningSpan> BuildBackgroundPositioningSpans(CTLine line)
        {
            var bgSpans = new List<BackgroundColorPositioningSpan>();

            foreach (var span in _backgroundSpans)
            {
                TextLineExtensions.GetCTLineRange(line, span.Range, out SpanRange<nfloat> lineRange);
                bgSpans.Add(new BackgroundColorPositioningSpan(lineRange, span.Color));
            }

            return bgSpans;
        }

        private List<UnderlinePositioningSpan> BuildUnderlines(CTLine line)
        {
            var positioningSpans = new List<UnderlinePositioningSpan>();

            var ulPositioning = _font.UnderlinePosition;
            var ulThickness = _font.UnderlineThickness;

            foreach (var underlineSpan in _underlineRanges)
            {
                TextLineExtensions.GetCTLineRange(line, underlineSpan.Range, out SpanRange<nfloat> lineRange);

                var thickness = ulThickness;

                switch (underlineSpan.Style)
                {
                    case UnderlineStyle.Thick:
                        thickness *= 2;
                        break;
                    case UnderlineStyle.Single:
                    default:
                        break;
                }

                var yRange = new SpanRange<nfloat>((-ulPositioning - 0.5f) * thickness,
                                                    (-ulPositioning - 0.5f) * thickness);

                positioningSpans.Add(new UnderlinePositioningSpan(lineRange, yRange, underlineSpan.Style));
            }

            return positioningSpans;
        }
    }
}
