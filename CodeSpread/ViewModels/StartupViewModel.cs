using System.Collections.ObjectModel;
using System.Windows.Input;
using CodeSpread.Base;
using CodeSpread.Services;
using Microsoft.Win32;

namespace CodeSpread.ViewModels;

public class StartupViewModel
{
    private readonly RecentFileStream _recentFileStream;
    public ObservableCollection<string> RecentFiles { get; }
    public ICommand OpenFileCommand { get; }

    public StartupViewModel()
    {
        _recentFileStream = new RecentFileStream(maxRecentFiles: 20);
        RecentFiles = new ObservableCollection<string>(_recentFileStream.LoadRecentFiles());
        OpenFileCommand = new RelayCommand(OpenFile);
    }

    public void AddFile(string filePath)
    {
        _recentFileStream.AddRecentFile(filePath);

        // Clear and reload the ObservableCollection
        RecentFiles.Clear();
        foreach (var file in _recentFileStream.LoadRecentFiles())
        {
            RecentFiles.Add(file);
        }
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
                // Save the file path into RecentFileStream
                _recentFileStream.AddRecentFile(filePath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., logging)
                Console.WriteLine($"Failed to add file to recent list: {ex.Message}");
            }
        }
    }
}
