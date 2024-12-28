using System.Collections.ObjectModel;
using CodeSpread.Decompiler.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CodeSpread.ViewModels;

public class DecompileViewModel : INotifyPropertyChanged
{
    private string _selectedCode;
    private readonly Dictionary<string, int> _codeMap = new();
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

            // For future 

            //foreach (var type in module.DecompiledTypes)
            //{
            //    // Map properties and methods to their positions
            //    int currentLine = 0;
            //    foreach (var line in type.DecompiledCode.Split('\n'))
            //    {
            //        currentLine++;
            //        if (type.Properties.Contains(line.Trim()))
            //        {
            //            _codeMap[line.Trim()] = currentLine;
            //        }
            //        if (type.Methods.Contains(line.Trim()))
            //        {
            //            _codeMap[line.Trim()] = currentLine;
            //        }
            //    }
            //}
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
