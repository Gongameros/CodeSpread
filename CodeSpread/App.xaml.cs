using Microsoft.Extensions.Hosting;
using System.Windows;
using CodeSpread.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using CodeSpread.Views;

namespace CodeSpread;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {

    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            base.OnStartup(e);
        }
        catch (Exception ex)
        {
            // Log the exception or inspect it
            Debug.WriteLine(ex.Message);
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}
