// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Vanalyze.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Vanalyze : Vanilla Analyze, 基于OSS同步的多端设备使用情况统计分析工具";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "启动",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Play24 },
                TargetPageType = typeof(Views.Pages.StartPage)
            },
            new NavigationViewItem()
            {
                Content = "分类",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Tag24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "统计分析",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataArea24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "状态",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataUsage24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "设置",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
