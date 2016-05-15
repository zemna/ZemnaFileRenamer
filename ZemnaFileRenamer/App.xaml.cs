using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System.Diagnostics;
using System;

namespace ZemnaFileRenamer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if ((e.Args.Length > 1) && (e.Args[0] == "-uninstall"))
            {
                Process uinst = new Process();
                uinst.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\msiexec";
                uinst.StartInfo.Arguments = "/x" + e.Args[1];
                uinst.Start();

                this.Shutdown();
            }
            else
            {
                MainWindow win = new MainWindow();
                win.Show();
            }
        }
    }
}
