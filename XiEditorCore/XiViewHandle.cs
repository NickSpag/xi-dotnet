using System;
using XiEditorCore.Enums;
using XiEditorCore.Models;

namespace XiEditorCore
{
    /// <summary>
    /// This is a handle in to the core's concept of a view, which is core's understanding of what text is currently visible.
    /// </summary>
    public class XiViewHandle
    {
        readonly XiCoreHandle xiCore;

        public string Id { get; }

        public XiViewHandle(XiCoreHandle core, string viewId)
        {
            xiCore = core;
            Id = viewId;
        }

        // right now this is from the mac doCommand thing
        public void PerformCommand(EditSubMethodWithoutParameters subMethod)
        {
            xiCore.SendNotification(new EditWithoutSubmethodParameters(Id, subMethod));
        }

        #region Editing
        public void Delete()
        {
            xiCore.SendNotification(new EditWithoutSubmethodParameters(Id, EditSubMethodWithoutParameters.DeleteBackward));
        }

        public void Delete(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Delete();
            }
        }

        public void Insert(string text)
        {
            xiCore.SendNotification(new EditWithSubmethodParameters<InsertParameters>(Id, new InsertParameters(text)));
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
