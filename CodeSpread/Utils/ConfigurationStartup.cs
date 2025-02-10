using CodeSpread.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSpread.Utils;
public static class ConfigurationStartup
{

    public static void ApplyDefaultConfiguration(IServiceProvider serviceProvider)
    {
        SettingsStore settingsStore = serviceProvider.GetRequiredService<SettingsStore>();

        AppThemeUtility.ChangeTheme(settingsStore.Theme);
    }
}

