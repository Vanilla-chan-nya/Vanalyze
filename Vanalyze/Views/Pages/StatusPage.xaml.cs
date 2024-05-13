// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Vanalyze.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Vanalyze.Views.Pages
{
    public partial class StatusPage : INavigableView<StatusPageViewModel>
    {
        public StatusPageViewModel ViewModel { get; }

        public StatusPage(StatusPageViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
