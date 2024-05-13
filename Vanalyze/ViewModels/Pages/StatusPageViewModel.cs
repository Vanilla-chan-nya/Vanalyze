using System.Windows.Media;
using Vanalyze.Models;
using Wpf.Ui.Controls;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;
using System.Diagnostics;
using Vanalyze.Services;

namespace Vanalyze.ViewModels.Pages
{
    public partial class StatusPageViewModel : ObservableObject
    {
        private readonly DispatcherTimer _timer;
        private readonly ActivateWindowRecordService _activateWindowRecordService;

        [ObservableProperty]
        private string _activeWindowTitle;

        [ObservableProperty]
        private string _activeProcessName;

        public StatusPageViewModel()
        {
            // Get the singleton instance of the service
            _activateWindowRecordService = ActivateWindowRecordService.Instance;

            // Setup a timer to refresh the UI
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get the latest record, if available
            var latestRecord = _activateWindowRecordService.Records.LastOrDefault();

            if (!string.IsNullOrEmpty(latestRecord.WindowTitle) || !string.IsNullOrEmpty(latestRecord.ProcessName))
            {
                ActiveWindowTitle = latestRecord.WindowTitle;
                ActiveProcessName = latestRecord.ProcessName;
            }
            else
            {
                ActiveWindowTitle = "No active window";
                ActiveProcessName = "No active process";
            }
        }
    }
}
