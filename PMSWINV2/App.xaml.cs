using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;
using System.Windows.Threading;
using System.Timers;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using MTPMSWIN.View;
 
namespace MTPMSWIN
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
         [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            //DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName = DevExpress.Xpf.Core.Theme.MetropolisLightName;             
            //DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.MetropolisLightName;
            //DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.LightGrayName;
            try{
                DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2007BlueName;
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;            
            }
            catch { DevExpress.Xpf.Core.DXMessageBox.Show("Không thể thiết lập mẫu giao diện cho ứng dụng..", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information); }
            
            base.OnStartup(e);
        }
    }
}
