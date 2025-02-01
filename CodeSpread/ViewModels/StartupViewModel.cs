using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CodeSpread.Base;
using CodeSpread.Commands;
using CodeSpread.Services;
using CodeSpread.Stores;
using Microsoft.Win32;

namespace CodeSpread.ViewModels;

public class StartupViewModel : ViewModelBase
{
    private readonly RecentFileStream _recentFileStream;
    private readonly SelectedFileStore _selectedFileStore;
    private readonly INavigationService _decompileNavigationService;

    public ObservableCollection<string> RecentFiles { get; }
    public ICommand OpenFileCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand OpenRecentFileCommand { get; }

    public StartupViewModel(RecentFileStream recentFileStream, SelectedFileStore selectedFileStore,
        INavigationService aboutNavigationService, INavigationService decompileNavigationService)
    {
        _recentFileStream = recentFileStream;
        _selectedFileStore = selectedFileStore;
        _decompileNavigationService = decompileNavigationService;

        RecentFiles = new ObservableCollection<string>(_recentFileStream.LoadRecentFiles());
        OpenFileCommand = new RelayCommand(OpenFile);
        OpenRecentFileCommand = new RelayCommand<string>(OpenRecentFile);
        AboutCommand = new NavigateCommand(aboutNavigationService);

    }

    private void OpenFile()
    {
        // Open OpenFileDialog to select a file
        var openFileDialog = new OpenFileDialog
        {
            Filter = "All Files|*.*",
            Title = "Select a File"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;

            try
            {
                _recentFileStream.AddRecentFile(filePath);
                _selectedFileStore.SelectedFile = filePath;

                _decompileNavigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add file to recent list: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }

    private void OpenRecentFile(string filePath)
    {
        try
        {
            _selectedFileStore.SelectedFile = filePath;

            _decompileNavigationService.Navigate();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to open recent file: {ex.Message}",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
        }
    }
}
