using System.Windows;
using ZemnaFileRenamer.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using ZemnaFileRenamer.Message;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZemnaFileRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            Messenger.Default.Register<RuleSettingFocusMessage>(this, OnRuleSettingFocusMessage);
            Messenger.Default.Register<PreviewMessage>(this, OnPreviewMessage);
        }

        private void OnRuleSettingFocusMessage(RuleSettingFocusMessage msg)
        {

        }

        private void OnPreviewMessage(PreviewMessage msg)
        {
            PreviewWindow win = new PreviewWindow();

            win.DataContext = msg.ViewModel;
            win.Owner = this;

            win.ShowDialog();
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
                overflowGrid.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
