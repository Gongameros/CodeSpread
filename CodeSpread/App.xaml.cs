using Microsoft.Extensions.Hosting;
using System.Windows;
using CodeSpread.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using CodeSpread.Views;
using System.Runtime.InteropServices.JavaScript;

namespace CodeSpread;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddCodeSpread();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            base.OnStartup(e);

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
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
        _host.Dispose();
        base.OnExit(e);
    }
}
