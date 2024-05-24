using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Vanalyze.Models.Vanalyze.Models;
namespace Vanalyze.Services
{
    internal class ActivateWindowRecordService
    {

        private static ActivateWindowRecordService _instance;
        private DispatcherTimer _timer;
        private List<WindowRecord> _records = new List<WindowRecord>();

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private const string FilePath = "ActiveWindowRecords.txt"; // 文件路径

        private ActivateWindowRecordService()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };//每隔几秒检测一次
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        public static ActivateWindowRecordService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ActivateWindowRecordService();
                }
                return _instance;
            }
        }

        public IReadOnlyList<WindowRecord> Records => _records.AsReadOnly();

        private void Timer_Tick(object sender, EventArgs e)
        {
            IntPtr handle = GetForegroundWindow();
            string title = GetActiveWindowTitle(handle);
            string processName = GetActiveProcessName(handle);
            var record = new WindowRecord(title, processName);
            _records.Add(record);

            //file write
            WriteRecordToFile(record);
        }


        private string GetActiveWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "No active window";
        }

        private string GetActiveProcessName(IntPtr handle)
        {
            uint processId;
            GetWindowThreadProcessId(handle, out processId);
            Process process = Process.GetProcessById((int)processId);
            return process.MainModule.FileName;
        }


        private void WriteRecordToFile(WindowRecord record)
        {
            try
            {
                string timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                string line = $"{timestamp} {record.ProcessName}";

                // 使用 FileStream 以独占方式打开文件
                using (var fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                // 处理文件写入异常，例如记录日志
                Debug.WriteLine($"Failed to write record to file: {ex.Message}");
            }
        }

        private void SetFilePermissions(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                var fileSecurity = fileInfo.GetAccessControl();

                // 当前用户
                var currentUser = WindowsIdentity.GetCurrent().Name;

                // 允许当前用户读取和写入，不允许修改
                var readWriteRule = new FileSystemAccessRule(currentUser,
                    FileSystemRights.Read | FileSystemRights.Write,
                    AccessControlType.Allow);

                var denyModifyRule = new FileSystemAccessRule(currentUser,
                    FileSystemRights.Modify,
                    AccessControlType.Deny);

                fileSecurity.AddAccessRule(readWriteRule);
                fileSecurity.AddAccessRule(denyModifyRule);

                fileInfo.SetAccessControl(fileSecurity);
            }
            catch (Exception ex)
            {
                // 处理权限设置异常，例如记录日志
                Debug.WriteLine($"Failed to set file permissions: {ex.Message}");
            }
        }

    }
}
