using CodeSpread.Commands;
using CodeSpread.Services;
using System.Windows.Input;

namespace CodeSpread.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public ICommand NavigateStartupCommand { get; }

    public SettingsViewModel(INavigationService startupNavigationService)
    {
        NavigateStartupCommand = new NavigateCommand(startupNavigationService);
    }
}
