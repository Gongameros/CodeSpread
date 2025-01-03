﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CodeSpread.Base;
using CodeSpread.Decompiler;
using CodeSpread.Decompiler.Models;
using CodeSpread.Services;
using Microsoft.Win32;

namespace CodeSpread.ViewModels;

public class StartupViewModel
{
    private readonly RecentFileStream _recentFileStream;
    public ObservableCollection<string> RecentFiles { get; }
    public ICommand OpenFileCommand { get; }
    public ICommand OpenRecentFileCommand { get; }

    public StartupViewModel()
    {
        _recentFileStream = new RecentFileStream(maxRecentFiles: 20);
        RecentFiles = new ObservableCollection<string>(_recentFileStream.LoadRecentFiles());
        OpenFileCommand = new RelayCommand(OpenFile);
        OpenRecentFileCommand = new RelayCommand<string>(OpenRecentFile);
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

                // Decompiling the file 
                AssemblyInformation assemblyInformation = FileDecompiler.DecompileAssembly(filePath);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.OnFileSelected(assemblyInformation);
                
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
            // Decompiling the recent file
            AssemblyInformation assemblyInformation = FileDecompiler.DecompileAssembly(filePath);
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.OnFileSelected(assemblyInformation);
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
