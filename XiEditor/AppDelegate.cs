using System;
using AppKit;
using Foundation;
using XiEditorCore;

namespace XiEditor
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        private XiCoreHandle xiCore;

        public bool IsDebug { get; private set; }

        public XiCoreHandle XiCore { get; set; }

        public AppDelegate()
        {
            #if DEBUG
            IsDebug = true;
#endif

            XiCore = new XiCoreHandle();

            //xiCore = new XiCoreBridge();

        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
            //try
            //{
            //    xiCore.Test();                
            //}
            //catch (System.Exception ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //}
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }

    }
}
