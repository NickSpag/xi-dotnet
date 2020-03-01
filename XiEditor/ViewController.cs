#nullable enable
using System;
using System.Security.Policy;
using AppKit;
using Foundation;
using ObjCRuntime;
using XiEditorCore;

namespace XiEditor
{
    public partial class ViewController : NSViewController, INSStandardKeyBindingResponding
    {
        readonly XiCoreHandle xiCore;

        public new SimplifiedView View => (SimplifiedView)base.View;

        public ViewController(IntPtr handle) : base(handle)
        {
            if (NSApplication.SharedApplication.Delegate is AppDelegate @delegate)
            {
                xiCore = @delegate.XiCore;
            }
            else throw new InvalidCastException();
        }

        public override bool AcceptsFirstResponder() => true;

        public async override void ViewDidLoad()
        {
            View.XiView ??= await xiCore.InstantiateView() ?? throw new Exception("Unable to aquire a view");
        }

        public override void KeyDown(NSEvent theEvent)
        {
            View.InputContext.HandleEvent(theEvent);
        }

        async partial void pressButton(NSObject sender)
        {

        }

        [Export("doCommandBySelector:")]
        public void DoCommandBySelector(Selector selector)
        {
            View.HandleCommand(selector.Name);
        }

        
    }
}
