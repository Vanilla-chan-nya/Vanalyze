using System.Windows.Media;
using Vanalyze.Models;
using Wpf.Ui.Controls;

namespace Vanalyze.ViewModels.Pages
{
    public partial class StartPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _counter = 0;

        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }
    }
}
