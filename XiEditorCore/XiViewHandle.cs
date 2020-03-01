using System;
using XiEditorCore.Enums;

namespace XiEditorCore
{
    public class XiViewHandle
    {
        readonly XiCoreHandle xiCore;

        public string Id { get; }

        public XiViewHandle(XiCoreHandle core, string viewId)
        {
            xiCore = core;
            Id = viewId;
        }

        #region Editing
        public void Insert()
        {

        }

        public void Paste()
        {

        }

        public void Copy()
        {

        }

        public void Cut()
        {

        }

        public void Scroll()
        {

        }

        public void Resize()
        {

        }

        public void Gesture()
        {

        }

        public void GotoLine()
        {

        }
        #endregion

    }
}
