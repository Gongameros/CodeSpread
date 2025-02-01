using CodeSpread.Decompiler;
using CodeSpread.Decompiler.Models;
using CodeSpread.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.IO;

namespace CodeSpread.ViewModels;

public class DecompileViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly SelectedFileStore _selectedFileStore;

    private string _selectedCode;
    public ObservableCollection<AssemblyModule> AssemblyModules { get; set; } = new ObservableCollection<AssemblyModule>();

    public string SelectedCode
    {
        get => _selectedCode;
        set
        {
            _selectedCode = value;
            OnPropertyChanged();
        }
    }

    public DecompileViewModel(SelectedFileStore selectedFileStore)
    {
        _selectedFileStore = selectedFileStore;

        try
        {
            if (File.Exists(_selectedFileStore.SelectedFile))
            {
                AssemblyInformation decompiledAssembly = FileDecompiler.DecompileAssembly(_selectedFileStore.SelectedFile);
                foreach (var module in decompiledAssembly.AssemblyModules)
                {
                    AssemblyModules.Add(module);
                }
            }
            else
            {
                MessageBox.Show($"No file path specified in selected file store",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Decompiling assembly error: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override void Dispose()
    {
        _selectedFileStore.ClearSelectedFile();
        base.Dispose();
    }
}
