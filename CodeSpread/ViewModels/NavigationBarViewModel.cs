using CodeSpread.Commands;
using CodeSpread.Services;
using System.Windows.Input;

namespace CodeSpread.ViewModels;

public class NavigationBarViewModel : ViewModelBase
{
    private readonly RecentFileStream _recentFileStream;

    public ICommand NavigateAboutCommand { get; }
    public ICommand NavigateStartupCommand { get; }
    public ICommand NavigateDecompileCommand { get; }
    public ICommand NavigateSettingsCommand { get; }

    public NavigationBarViewModel(RecentFileStream recentFileStream,
        INavigationService aboutNavigationService,
        INavigationService startupNavigationService,
        INavigationService settingsNavigationService)
    {
        _recentFileStream = recentFileStream;
        NavigateAboutCommand = new NavigateCommand(aboutNavigationService);
        NavigateStartupCommand = new NavigateCommand(startupNavigationService);
        NavigateSettingsCommand = new NavigateCommand(settingsNavigationService);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
