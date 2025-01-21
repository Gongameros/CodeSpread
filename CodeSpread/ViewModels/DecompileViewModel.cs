using System.Collections.ObjectModel;
using CodeSpread.Decompiler.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CodeSpread.ViewModels;

public class DecompileViewModel : INotifyPropertyChanged
{
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

    public DecompileViewModel(AssemblyInformation decompiledAssembly)
    {
        foreach (var module in decompiledAssembly.AssemblyModules)
        {
            AssemblyModules.Add(module);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
