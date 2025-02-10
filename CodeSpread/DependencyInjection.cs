using Microsoft.Extensions.DependencyInjection;
using CodeSpread.ViewModels;
using CodeSpread.Services;
using Microsoft.Extensions.Configuration;
using CodeSpread.Stores;
using CodeSpread.Utils;

namespace CodeSpread;

public static class DependencyInjection
{
    public static IServiceCollection AddCodeSpread(this IServiceCollection services)
    {
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<SelectedFileStore>();
        services.AddSingleton<SettingsStore>();

        // Setting Default View
        services.AddSingleton<INavigationService>(s => NavigationUtility.CreateStartupNavigationService(s));

        // Added Stream for reading recent files
        services.AddTransient<RecentFileStream>();

        // Add StartupViewModel
        services.AddTransient<StartupViewModel>(s =>
            new StartupViewModel(s.GetRequiredService<RecentFileStream>(),
            s.GetRequiredService<SelectedFileStore>(),
            NavigationUtility.CreateAboutNavigationService(s),
            NavigationUtility.CreateDecompileNavigationService(s),
            NavigationUtility.CreateSettingsNavigationService(s)));

        // Add AboutViewModel
        services.AddTransient<AboutViewModel>(s =>
            new AboutViewModel(NavigationUtility.CreateStartupNavigationService(s)));

        // Add DecompileViewModel
        services.AddTransient<DecompileViewModel>(s =>
            new DecompileViewModel(s.GetRequiredService<SelectedFileStore>()));

        // Add SettingsViewModel
        services.AddTransient<SettingsViewModel>(s =>
            new SettingsViewModel(NavigationUtility.CreateStartupNavigationService(s),
                s.GetRequiredService<SettingsStore>()));

        // Add NavBarModel
        services.AddTransient<NavigationBarViewModel>(NavigationUtility.CreateNavigationBarViewModel);

        // Add MainWindow and MainViewModel
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>(s => new MainWindow()
        {
            DataContext = s.GetRequiredService<MainViewModel>()
        });


        return services;
    }

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        return services;
    }
}
