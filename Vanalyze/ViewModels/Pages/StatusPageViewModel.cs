using System.Windows.Media;
using Vanalyze.Models;
using Wpf.Ui.Controls;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;

namespace Vanalyze.ViewModels.Pages
{
    public partial class StatusPageViewModel : ObservableObject
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private DispatcherTimer _timer;

        [ObservableProperty]
        private string _activeWindowTitle;

        public StatusPageViewModel()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ActiveWindowTitle = GetActiveWindowTitle();
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "No active window";
        }
    }
}
