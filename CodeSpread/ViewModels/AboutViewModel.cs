using CodeSpread.Base;
using System.Diagnostics;
using System.Windows.Input;

namespace CodeSpread.ViewModels;

public class AboutViewModel
{
    public ICommand OpenGitHubCommand { get; }

    public AboutViewModel()
    {
        OpenGitHubCommand = new RelayCommand(OpenGithub);
    }

    private void OpenGithub()
    {
        Process.Start(new ProcessStartInfo("https://github.com/Gongameros/CodeSpread") { UseShellExecute = true });
    }
}
