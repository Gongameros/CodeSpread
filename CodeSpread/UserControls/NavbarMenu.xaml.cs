using CodeSpread.Decompiler.Models;
using CodeSpread.Decompiler;
using CodeSpread.Services;
using CodeSpread.Views;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSpread.UserControls;

/// <summary>
/// Interaction logic for NavbarMenu.xaml
/// </summary>
public partial class NavbarMenu : UserControl
{
    public NavbarMenu()
    {
        InitializeComponent();
    }

    //private void OpenFile_Click(object sender, RoutedEventArgs e)
    //{
    //    // Open OpenFileDialog to select a file
    //    var openFileDialog = new OpenFileDialog
    //    {
    //        Filter = "All Files|*.*",
    //        Title = "Select a File"
    //    };

    //    if (openFileDialog.ShowDialog() == true)
    //    {
    //        string filePath = openFileDialog.FileName;

    //        try
    //        {
    //            // Save the file path into RecentFileStream
    //            _recentFileStream.AddRecentFile(filePath);

    //            // Decompiling the file 
    //            AssemblyInformation assemblyInformation = FileDecompiler.DecompileAssembly(filePath);
    //            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
    //            mainWindow.OnFileSelected(assemblyInformation);

    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Failed to add file to recent list: {ex.Message}",
    //                            "Error",
    //                            MessageBoxButton.OK,
    //                            MessageBoxImage.Error);
    //        }
    //    }
    //}
}
