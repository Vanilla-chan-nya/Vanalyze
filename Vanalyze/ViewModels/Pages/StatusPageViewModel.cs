using System.Windows.Media;
using Vanalyze.Models;
using Wpf.Ui.Controls;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;
using System.Diagnostics;

namespace Vanalyze.ViewModels.Pages
{
    public partial class StatusPageViewModel : ObservableObject
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private DispatcherTimer _timer;

        [ObservableProperty]
        private string _activeWindowTitle;

        [ObservableProperty]
        private string _activeProcessName;

        public StatusPageViewModel()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IntPtr handle = GetForegroundWindow();
            ActiveWindowTitle = GetActiveWindowTitle(handle);
            ActiveProcessName = GetActiveProcessName(handle);
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
