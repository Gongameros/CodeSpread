using CodeSpread.Decompiler.Models;
using CodeSpread.Views;
using System.ComponentModel;
using System.Windows;

namespace CodeSpread;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public object _currentView { get; set; }
    public object CurrentView
    {
        get { return _currentView; }
        set
        {
            if (_currentView != value)
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); // Notify the UI of the change
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        // Set initial view to StartupView
        CurrentView = new StartupView();
    }

    // Method to handle file selection (triggered on file selection in StartupView)
    public void OnFileSelected(AssemblyInformation assemblyInformation)
    {
        // When a file is selected, switch to DecompileView
        CurrentView = new DecompileView(assemblyInformation);
    }
}