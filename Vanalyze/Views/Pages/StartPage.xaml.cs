using Vanalyze.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Vanalyze.Views.Pages
{
    public partial class StartPage : INavigableView<StartPageViewModel>
    {
        public StartPageViewModel ViewModel { get; }

        public StartPage(StartPageViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
