using CodeSpread.Views;
using Microsoft.Extensions.DependencyInjection;
using CodeSpread.ViewModels;
using CodeSpread.Services;
using CodeSpread.UserControls;
using Microsoft.Extensions.Configuration;

namespace CodeSpread;

public static class DependencyInjection
{
    public static IServiceCollection AddCodeSpread(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<AboutView>();
        services.AddSingleton<AboutViewModel>();
        services.AddSingleton<RecentFileStream>();
        services.AddSingleton<NavbarMenu>();
        services.AddTransient<StartupView>();
        services.AddTransient<StartupViewModel>();

        return services;
    }

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        return services;
    }
}
