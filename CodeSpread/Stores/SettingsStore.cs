using CodeSpread.Utils;
using System.Windows;

namespace CodeSpread.Stores
{
    public class SettingsStore
    {
        private Uri _theme;

        public Uri Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                ApplyTheme();
                SaveThemeToUserSettings(value);
                CurrentThemeChanged?.Invoke();
            }
        }

        public event Action CurrentThemeChanged;

        public SettingsStore()
        {
            LoadThemeFromUserSettings();
        }

        private void ApplyTheme()
        {
            AppThemeUtility.ChangeTheme(_theme);
        }

        private void LoadThemeFromUserSettings()
        {
            string savedTheme = Properties.Settings.Default.Theme;
            _theme = new Uri($"Themes/{savedTheme}.xaml", UriKind.Relative);
        }

        private void SaveThemeToUserSettings(Uri newThemeUri)
        {
            string themeName = System.IO.Path.GetFileNameWithoutExtension(newThemeUri.ToString());

            // Save new theme to user settings
            Properties.Settings.Default.Theme = themeName;
            Properties.Settings.Default.Save();
        }
    }
}