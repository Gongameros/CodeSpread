using CodeSpread.ViewModels;
using System.Windows.Controls;

namespace CodeSpread.Views;

/// <summary>
/// Interaction logic for StartupView.xaml
/// </summary>
public partial class StartupView : UserControl
{
    public StartupView()
    {
        InitializeComponent();
        DataContext = new StartupViewModel();
    }
}
