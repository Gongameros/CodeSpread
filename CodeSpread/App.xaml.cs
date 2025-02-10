using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;
using CodeSpread.Services;
using System;
using CodeSpread.ViewModels;
using CodeSpread.Stores;
using CodeSpread.Utils;

namespace CodeSpread;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    public IConfiguration Configuration { get; private set; }

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddConfiguration(Configuration);
                services.AddCodeSpread();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            // Applying default configuration from Properties
            ConfigurationStartup.ApplyDefaultConfiguration(_host.Services);

            INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

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
        _host.Dispose();
        base.OnExit(e);
    }
}
