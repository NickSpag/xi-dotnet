using System;
using Foundation;
using AppKit;
using CoreGraphics;

namespace XiEditor
{
    public class LineCache { }
    public class StyleMap { }
    public class Theme { }

    public interface ITextPlaneDataSource
    {
        LineCache LineCache { get; }
        StyleMap StyleMap { get; }
        Theme Theme { get; }
        TextDrawingMetrics TextDrawingMetrics { get; }

        //todo Document?
        //todo XiViewProxy - this looks to be a reference to a specific "view" (and view-id-1 etc) within the core.

        float GutterWidth { get; }
        CGPoint ScrollOrigin { get; }

        void MaxWidthChange(double toWidth);
    }

    public partial class TextPlaneViewController : NSViewController, ITextPlaneDataSource
    {
        //strongly typed view accessor
        public new TextPlaneView View => (TextPlaneView)base.View;

        // ITextPlaneDataSource
        public LineCache LineCache => throw new NotImplementedException();
        public StyleMap StyleMap => throw new NotImplementedException();
        public Theme Theme => throw new NotImplementedException();
        public TextDrawingMetrics TextDrawingMetrics => throw new NotImplementedException();
        public float GutterWidth => throw new NotImplementedException();
        public CGPoint ScrollOrigin => throw new NotImplementedException();

        #region Constructors
        // Called when created from unmanaged code
        public TextPlaneViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public TextPlaneViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        public override void ViewDidLoad()
        {
            View.DataSource = this;
        }

        public void MaxWidthChange(double toWidth)
        {
            throw new NotImplementedException();
        }
        #endregion

        void test()
        {

        }
    }
}
