using CodeSpread.Base;
using CodeSpread.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeSpread.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        private readonly AboutViewModel _aboutViewModel;

        public AboutView()
        {
            InitializeComponent();

            _aboutViewModel = new AboutViewModel();

            this.DataContext = _aboutViewModel;
        }

        private void ReturnBackIcon_Click(object sender, MouseButtonEventArgs e)
        {
            // Navigate back to the Main Menu
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.CurrentView = new StartupView();
            }
        }
    }
}
