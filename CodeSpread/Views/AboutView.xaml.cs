using CodeSpread.Base;
using CodeSpread.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;

        public AboutView(AboutViewModel aboutViewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _aboutViewModel = aboutViewModel;
            _serviceProvider = serviceProvider;

            this.DataContext = _aboutViewModel;
        }

        private void ReturnBackIcon_Click(object sender, MouseButtonEventArgs e)
        {
            // Navigate back to the Main Menu
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                StartupView startupView = _serviceProvider.GetRequiredService<StartupView>();
                mainWindow.CurrentView = startupView;
            }
        }
    }
}
