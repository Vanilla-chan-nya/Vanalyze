using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;
using Wpf.Ui.Controls;
namespace Vanalyze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            var titleBar = FindName("TitleBar") as TitleBar;
            if (titleBar != null)
            {
                titleBar.Title += "\tCompileTime=" + GetCompileVersion();
            }

        }

        string GetCompileVersion()
        {
            string OriginVersion = "" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);
            int MsgCnt = 0;
            string year = "";
            string month = "";
            string day = "";
            string hour = "";
            string min = "";
            string sec = "";
            for (int i = 0; i < OriginVersion.Length && MsgCnt < 6; i++)
            {
                char ch = OriginVersion[i];
                if (ch >= '0' && ch <= '9')
                {
                    switch (MsgCnt)
                    {
                        case 0: year += ch; break;
                        case 1: month += ch; break;
                        case 2: day += ch; break;
                        case 3: hour += ch; break;
                        case 4: min += ch; break;
                        case 5: sec += ch; break;
                    }
                }
                else
                {
                    MsgCnt++;
                }
            }
            while (year.Length < 4) year = "0" + year;
            while (month.Length < 2) month = "0" + month;
            while (day.Length < 2) day = "0" + day;
            while (hour.Length < 2) hour = "0" + hour;
            while (min.Length < 2) min = "0" + min;
            while (sec.Length < 2) sec = "0" + sec;
            return year + month + day + hour + min + sec;
        }




    }
}