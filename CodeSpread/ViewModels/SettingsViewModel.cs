using System.Collections.ObjectModel;
using CodeSpread.Commands;
using CodeSpread.Services;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CodeSpread.Base;
using CodeSpread.Stores;
using CodeSpread.Utils;

namespace CodeSpread.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly SettingsStore _settingsStore;
    private string _selectedTheme;
    public ICommand NavigateStartupCommand { get; }
    public ICommand SaveChangesCommand { get; }

    public ObservableCollection<string> Themes { get; } = new ObservableCollection<string>
    {
        "Dark", "Light", "System Default"
    };

    public string SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            _selectedTheme = value;
            OnPropertyChanged(nameof(SelectedTheme));
        }
    }


    public SettingsViewModel(INavigationService startupNavigationService, SettingsStore settingStore)
    {
        _settingsStore = settingStore;
        NavigateStartupCommand = new NavigateCommand(startupNavigationService);
        SaveChangesCommand = new RelayCommand(SaveChanges);
        SelectedTheme = GetThemeName(_settingsStore.Theme);
    }

    private string GetThemeName(Uri themeUri)
    {
        return themeUri.ToString().Contains("Dark") ? "Dark" :
            themeUri.ToString().Contains("Light") ? "Light" :
            "System Default";
    }

    public void SaveChanges()
    {
        _settingsStore.Theme = GetThemeUri(SelectedTheme);
    }

    private Uri GetThemeUri(string themeName)
    {
        return themeName switch
        {
            "Dark" => new Uri("Themes/Dark.xaml", UriKind.Relative),
            "Light" => new Uri("Themes/Light.xaml", UriKind.Relative),
            "System Default" => new Uri(GetSystemThemePath(), UriKind.Relative),
            _ => new Uri(GetSystemThemePath(), UriKind.Relative)
        };
    }

    private string GetSystemThemePath()
    {
        string themeColor = SystemSettings.GetSystemDefaultTheme();
        return $"Themes/{themeColor}.xaml";
    }
}
