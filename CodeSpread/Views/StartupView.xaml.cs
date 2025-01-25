using CodeSpread.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CodeSpread.Views;

/// <summary>
/// Interaction logic for StartupView.xaml
/// </summary>
public partial class StartupView : UserControl
{
    private readonly StartupViewModel _viewModel;

    public StartupView(StartupViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
