using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Vanalyze.Models.Vanalyze.Models;
using Wpf.Ui;

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

        private ActivateWindowRecordService()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
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
            _records.Add(new WindowRecord(title, processName));
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



    }
}
