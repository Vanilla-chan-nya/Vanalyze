using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Vanalyze.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Vanalyze.Views.Pages
{
    public partial class ClassifyPage : INavigableView<ClassifyPageViewModel>
    {
        public ClassifyPageViewModel ViewModel { get; }

        public ClassifyPage(ClassifyPageViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
