using System.Collections.ObjectModel;
using CodeSpread.Decompiler;
using CodeSpread.Decompiler.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

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

    public DecompileViewModel()
    {
        // Load the assembly and populate the tree
        var assemblyPath = "D:\\Projects\\CodeSpreadProject\\CodeSpread.Decompiler\\bin\\Debug\\net8.0\\CodeSpread.Decompiler.dll";
        if (!Path.Exists(assemblyPath))
        {
            throw new FileNotFoundException($"Assembly path is invalid.\n{assemblyPath}");
        }
        var decompiledAssembly = FileDecompiler.DecompileAssembly(assemblyPath);

        foreach (var module in decompiledAssembly.AssemblyModules)
        {
            AssemblyModules.Add(module);
        }
    }

    public void OnSelectedItemChanged(object selectedItem)
    {
        switch (selectedItem)
        {
            case DecompiledType type:
                SelectedCode = type.DecompiledCode;
                break;
            case NestedType nested:
                SelectedCode = $"Namespace: {nested.Namespace}\n\nProperties:\n{string.Join("\n", nested.Properties)}\n\nMethods:\n{string.Join("\n", nested.Methods)}";
                break;
            default:
                SelectedCode = string.Empty;
                break;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
