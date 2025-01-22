using CodeSpread.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSpread
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCodeSpread(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<StartupView>();
            return services;
        }
    }
}
