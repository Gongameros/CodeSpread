using Microsoft.Win32;

namespace CodeSpread.Utils;
public static class SystemSettings
{
    public static string GetSystemDefaultTheme()
    {
        try
        {
            const string registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string valueName = "AppsUseLightTheme";

            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(registryKeyPath);
            if (key?.GetValue(valueName) is int value)
            {
                return value == 0 ? "Dark" : "Light";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading system theme: {ex.Message}");
        }

        return "Light"; // Default fallback
    }
}
