using CodeSpread.Services;
using CodeSpread.Stores;
using CodeSpread.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSpread.Utils;

public static class NavigationUtility
{
    public static INavigationService CreateStartupNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<StartupViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),
            () => serviceProvider.GetRequiredService<StartupViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    public static INavigationService CreateAboutNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<AboutViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<AboutViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    public static INavigationService CreateDecompileNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<DecompileViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<DecompileViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    public static INavigationService CreateSettingsNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<SettingsViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<SettingsViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    public static NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
    {
        return new NavigationBarViewModel(
            serviceProvider.GetRequiredService<RecentFileStream>(),
            CreateAboutNavigationService(serviceProvider),
            CreateStartupNavigationService(serviceProvider),
            CreateSettingsNavigationService(serviceProvider));
    }
}
