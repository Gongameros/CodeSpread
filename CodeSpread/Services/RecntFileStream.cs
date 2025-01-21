using System.IO;
using Newtonsoft.Json;
using System.Windows;
using CodeSpread.Base;

namespace CodeSpread.Services;

public class RecentFileStream
{
    private const string _filePath = "RecentFiles.json";
    public RecentFileStream()
    {

        try
        {
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("ERROR ctor(): Creating RecentFileStream");
        }


    }

    /// <summary>
    /// Adds a new file to the recent files list.
    /// </summary>
    public void AddRecentFile(string filePath)
    {
        var recentFiles = LoadRecentFiles();

        // Remove duplicate if it already exists
        recentFiles.Remove(filePath);

        // Add the file to the top of the list
        recentFiles.Insert(0, filePath);

        SaveRecentFiles(recentFiles);
    }

    /// <summary>
    /// Loads the list of recent files.
    /// </summary>
    public List<string> LoadRecentFiles()
    {
        if (!File.Exists(_filePath))
        {
            return new List<string>();
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            // Return an empty list if there's an error (e.g., corrupted file)
            return new List<string>();
        }
    }

    /// <summary>
    /// Saves the list of recent files to disk.
    /// </summary>
    private void SaveRecentFiles(List<string> recentFiles)
    {
        try
        {
            var json = JsonConvert.SerializeObject(recentFiles, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            // Handle exceptions such as permission issues
            MessageBox.Show($"Failed to save recent files: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
