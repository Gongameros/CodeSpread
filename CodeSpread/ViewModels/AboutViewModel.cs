using CodeSpread.Base;
using CodeSpread.Commands;
using CodeSpread.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace CodeSpread.ViewModels;

public class AboutViewModel : ViewModelBase
{
    public ICommand OpenGitHubCommand { get; }
    public ICommand NavigateStartupCommand { get; }

    public AboutViewModel(INavigationService startupNavigationService)
    {
        OpenGitHubCommand = new RelayCommand(OpenGithub);
        NavigateStartupCommand = new NavigateCommand(startupNavigationService);
    }

    private void OpenGithub()
    {
        try
        {
            Process.Start(new ProcessStartInfo("https://github.com/Gongameros/CodeSpread") { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to open https://github.com/Gongameros/CodeSpread link in browser: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
