#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XiEditorCore.Models;
using static XiEditorCore.Constants;

namespace XiEditorCore
{
    public class MeasureWidthEventArgs
    {

    }

    public class UpdateEventArgs
    {
        public UpdateNotification Notification { get; }

        public UpdateEventArgs(UpdateNotification update)
        {
            Notification = update;
        }
    }

    public class ScrollToEventArgs
    {
        public ScrollToNotification Notification { get; }

        public ScrollToEventArgs(ScrollToNotification notification)
        {
            Notification = notification;
        }
    }

    public class XiCoreHandle
    {
        readonly List<XiViewHandle> Views = new List<XiViewHandle>();

        readonly ManualResetEvent stdout_completed = new ManualResetEvent(false);
        readonly ManualResetEvent stderr_completed = new ManualResetEvent(false);
        readonly Process process;
        readonly Stream input;

        ushort messageIndex = 0;
        CancellationTokenSource newViewWaitCancellation = new CancellationTokenSource();

        string viewId { get; set; }
        bool needsView { get; set; } = true;

        public XiCoreHandle()
        {
            var startInfo = ConfigureStartInfo();
            process = Process.Start(startInfo);
            ConfigureProcess(process);

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            input = process.StandardInput.BaseStream;

            SendNotification(new ClientStartedNotification());
        }

        public async Task<XiViewHandle?> InstantiateView()
        {
            await SendNewViewRequest();

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(.1), newViewWaitCancellation.Token);
            }
            catch (TaskCanceledException ex)
            {
                var view = new XiViewHandle(this, viewId);
                Views.Add(view);
                needsView = false;

                return view;
            }

            return null;
        }

        private ProcessStartInfo ConfigureStartInfo()
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = GetXiCoreBinaryPath();

            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            startInfo.StandardOutputEncoding = Encoding.UTF8;
            startInfo.StandardErrorEncoding = Encoding.UTF8;

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            return startInfo;
        }

        private string GetXiCoreBinaryPath() => Path.Combine(Directory.GetCurrentDirectory(), XiCoreStructure.ExecutableName);

        private void ConfigureProcess(Process process)
        {
            process.EnableRaisingEvents = true;

            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Exited += Process_Exited;
        }

        #region To Core
        public void SendNotification<T>(T notification) where T : IRPCNotification
        {
            var bytes = Encoding.UTF8.GetBytes("{\"method\":\"" + notification.Method + "\",\"params\":" + JsonSerializer.Serialize(notification) + "}\n");

            input.Write(bytes, 0, bytes.Length);
            input.Flush();
        }

        //todo generalize SendRequest for other requests
        private async Task SendNewViewRequest()
        {
            // everything going to core is chunked by a newline, despite not being valid json.
            var data = JsonSerializer.Serialize(new NewViewRequest(messageIndex++)) + '\n';
            var bytes = Encoding.UTF8.GetBytes(data);

            input.Write(bytes, 0, bytes.Length);
            await input.FlushAsync();
        }
        #endregion

        #region From Core
        //public event EventHandler<ScrollToEventArgs> NeedsScrollTo;

        // should return {"id":0, "result":[[28.0,8.0]]}
        //public event EventHandler<MeasureWidthEventArgs> NeedsMeasureWidth;

        private void HandleScrollToNotification(ScrollToNotification scrollTo)
        {
            if(Views.First(v => v.Id == scrollTo.Params.ViewId) is XiViewHandle matchingView)
            {
                matchingView.ReceivedScrollTo(scrollTo);
            }
        }

        private void HandleUpdateNotification(UpdateNotification update)
        {
            if(Views.First(v => v.Id == update.Params.ViewId) is XiViewHandle matchingView)
            {
                matchingView.ReceivedUpdate(update);
            }
        }

        private void HandleRawDataReceived(string data)
        {
            //request response
            if(data.StartsWith("{\"id"))
            {
                System.Console.WriteLine($"Request: {data}");

                if(needsView)
                {
                    var newView = JsonSerializer.Deserialize<NewViewResponse>(data);

                    viewId = newView.Result;
                    newViewWaitCancellation.Cancel();
                }
            }

            //notification
            else if(data.StartsWith("{\"method"))
            {
                System.Console.WriteLine($"Notification: {data}");

                var method = data.Substring(11);

                if(method.StartsWith("update"))
                {
                    if(!needsView)
                    {
                        var update = JsonSerializer.Deserialize<UpdateNotification>(data);
                        HandleUpdateNotification(update);
                    }
                }
                else if(method.StartsWith("scroll_to"))
                {
                    if(!needsView)
                    {
                        var scroll = JsonSerializer.Deserialize<ScrollToNotification>(data);
                        HandleScrollToNotification(scroll);
                    }
                }
                else
                {
                    System.Console.WriteLine($"    unhandled: {data}");
                }
            }
            else
            {
                System.Console.WriteLine($"UNKNOWN RECEIVED: {data}");
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
            if (e.Data is null)
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
        #endregion

    }
}