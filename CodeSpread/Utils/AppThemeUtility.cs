using System.Windows;

namespace CodeSpread.Utils;
public static class AppThemeUtility
{
    public static void ChangeTheme(Uri themeUri)
    {

        ResourceDictionary theme = new ResourceDictionary()
        {
            Source = themeUri,
        };

        Application.Current.Resources.Clear();
        Application.Current.Resources.MergedDictionaries.Add(theme);
    }
}