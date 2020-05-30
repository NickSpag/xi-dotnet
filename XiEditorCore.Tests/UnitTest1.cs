using System;
using Xunit;

namespace XiEditorCore.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void CreateRustView()
        {
            var viewHandle = new RustView();

            Assert.True(viewHandle.HasValidHandle);
        }

        [Fact]
        public void InsertTextInToView()
        {
            var viewHandle = new RustView();

            viewHandle.Insert("hello");            
        }
    }
}
