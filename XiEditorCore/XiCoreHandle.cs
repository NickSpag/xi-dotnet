#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XiEditorCore.Models;
using XiEditorCore.Models.Notifications.ToCore;

namespace XiEditorCore
{
    public class MeasureWidthEventArgs
    {

    }

    public class UpdateEventArgs
    {

    }

    public class ScrollToEventArgs
    {
        public int Line { get; }
        public int Column { get; }

        public ScrollToEventArgs(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }

    public class XiCoreHandle
    {
        readonly List<XiViewHandle> Views = new List<XiViewHandle>();

        const string directory = "/Users/nickspagnola/Projects/XiEditor/XiEditor/bin/Debug/XiEditor.app/Contents/Resources/";
        const string fileName = "xi-core.exe";

        const string unixTest = "/Users/nickspagnola/Development/Projects/xi-editor/rust/target/release/xi-core-unix";

        ManualResetEvent stdout_completed = new ManualResetEvent(false);
        ManualResetEvent stderr_completed = new ManualResetEvent(false);

        private readonly Process process;
        private readonly Stream input;
        private ushort messageIndex = 0;

        private CancellationTokenSource newViewWaitCancellation = new CancellationTokenSource();
        private string viewId { get; set; }
        private bool needsView { get; set; } = true;

        public XiCoreHandle()
        {
            var startInfo = ConfigureStartInfo();
            process = Process.Start(startInfo);
            ConfigureProcess(process);

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            input = process.StandardInput.BaseStream;

            SendClientStarted();
        }

        public async Task<XiViewHandle?> InstantiateView()
        {
            await SendNewViewRequest();

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2), newViewWaitCancellation.Token);
            }
            catch (TaskCanceledException ex)
            {
                var view = new XiViewHandle(this, viewId);
                Views.Add(view);

                return view;
            }

            return null;
        }

        public event EventHandler<ScrollToEventArgs> NeedsScrollTo;

        public event EventHandler<UpdateEventArgs> NeedsUpdate;

        // should return {"id":0, "result":[[28.0,8.0]]}
        public event EventHandler<MeasureWidthEventArgs> NeedsMeasureWidth;

        private void SendClientStarted()
        {
            var getStartedJson = JsonSerializer.Serialize(new ClientStartedNotification()) + '\n';
            var bytes = Encoding.UTF8.GetBytes(getStartedJson);

            input.Write(bytes, 0, bytes.Length);
            input.Flush();
        }

        private async Task SendNewViewRequest()
        {
            var data = JsonSerializer.Serialize(new NewViewRequest(messageIndex++)) + '\n';
            var bytes = Encoding.UTF8.GetBytes(data);

            input.Write(bytes, 0, bytes.Length);
            await input.FlushAsync();
        }

        #region Test
        public async Task TestSendInsertCharacter(string viewId)
        {
            var data = JsonSerializer.Serialize(new EditNotification(viewId, 'A')) + '\n';
            var bytes = Encoding.UTF8.GetBytes(data);
            input.Write(bytes, 0, bytes.Length);
            await input.FlushAsync();
        }
        #endregion

        private ProcessStartInfo ConfigureStartInfo()
        {
            var startInfo = new ProcessStartInfo();

            startInfo.FileName = unixTest;

            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            startInfo.StandardOutputEncoding = Encoding.UTF8;
            startInfo.StandardErrorEncoding = Encoding.UTF8;

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            return startInfo;
        }

        private void ConfigureProcess(Process process)
        {
            process.EnableRaisingEvents = true;

            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Exited += Process_Exited;
        }

        private void HandleRawDataReceived(string data)
        {
            if(needsView && data.StartsWith("{\"id"))
            {
                var newView = JsonSerializer.Deserialize<NewViewResponse>(data);

                needsView = false;
                newViewWaitCancellation.Cancel();
                viewId = newView.Result;
            }
            else
            {
                System.Console.WriteLine(data);
            }
        }

        #region Event Handlers
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data is null)
            {
                System.Console.WriteLine("OutputStream: null received, resetting");
                stdout_completed.Set();
            }
            else
            {
                HandleRawDataReceived(e.Data);
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            System.Console.WriteLine("Exited");
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(e.Data is null)
            {
                System.Console.WriteLine("ErrorStream: null received, resetting");
                stderr_completed.Set();
            }
            else
            {
                System.Console.WriteLine("ErrorStream: " + e.Data);
            }
        }
        #endregion
    }
}
