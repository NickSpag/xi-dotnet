#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XiEditorCore.Enums;
using XiEditorCore.Models;
using static XiEditorCore.Constants;

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

                return view;
            }

            return null;
        }

        #region From Core
        public event EventHandler<ScrollToEventArgs> NeedsScrollTo;

        public event EventHandler<UpdateEventArgs> NeedsUpdate;

        // should return {"id":0, "result":[[28.0,8.0]]}
        public event EventHandler<MeasureWidthEventArgs> NeedsMeasureWidth;
        #endregion

        #region To Core
        public void SendNotification<T>(T notification) where T: IRPCNotification
        {
            var notificationJson = "{\"method\":\"" + notification.Method + "\",\"params\":" + JsonSerializer.Serialize(notification) + "}\n";

            //var notificationJson = JsonSerializer.Serialize(new ClientStartedNotification()) + '\n';
            var bytes = Encoding.UTF8.GetBytes(notificationJson);

            input.Write(bytes, 0, bytes.Length);
            input.Flush();
        }

        // This is currently the only request you can send to the front end.
        private async Task SendNewViewRequest()
        {
            // everything going to core is chunked by a newline, despite not being valid json.
            var data = JsonSerializer.Serialize(new NewViewRequest(messageIndex++)) + '\n';
            var bytes = Encoding.UTF8.GetBytes(data);

            input.Write(bytes, 0, bytes.Length);
            await input.FlushAsync();
        }

        #endregion

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

        #region Obsolete for now
        private void CreateXiCoreDirectoryStructure()
        {
            var current = Directory.GetCurrentDirectory();

            var syntectBinaryDirectory = Path.Combine(current, XiCoreStructure.SyntectBinaryDirectory);

            if(!Directory.Exists(syntectBinaryDirectory))
            {
                var info = Directory.CreateDirectory(syntectBinaryDirectory);

                File.Move(current + XiCoreStructure.SyntectBinaryName, XiCoreStructure.SyntectBinaryPath);

                WriteResourceToAppContents(XiCoreStructure.SyntectBinaryName, XiCoreStructure.SyntectBinaryPath);
                WriteResourceToAppContents(XiCoreStructure.SyntectManifestName, XiCoreStructure.SyntectManifestPath);
            }
        }
        private string GetSyntectPluginPath()
        {
            
            //var currentDirectory = Directory.GetCurrentDirectory();


            //if (Path)

            //    var xiCoreBinaryPath = Path.Combine(currentDirectory, Constants.XiCoreStructure.SyntectPluginName);

            //if (File.Exists(xiCoreBinaryPath))
            //{
            //    return xiCoreBinaryPath;
            //}

            return "";
        }

        private void WriteResourceToAppContents(string name, string path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var embeddedResource in assembly.GetManifestResourceNames())
            {
                if(embeddedResource.EndsWith(name))
                {
                    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    using (var binaryStream = assembly.GetManifestResourceStream(embeddedResource))
                    {
                        binaryStream.Seek(0, SeekOrigin.Begin);
                        binaryStream.CopyTo(fileStream);
                    }
                }
            }
        }
        #endregion

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